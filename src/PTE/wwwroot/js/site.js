$(
    function () {

        //variable initialization
        var questionDuration = parseInt($("#questionDuration").val());
        var answerDuration;
        var sendUrl;


        var interval; //total time
        var mid; // point at which user recording starts
        var progress = 0;
        URL = window.URL || window.webkitURL; //webkitURL is deprecated but nevertheless
        var gumStream; //stream from getUserMedia()
        var rec; //Recorder.js object
        var input; //MediaStreamAudioSourceNode we'll be recording
        var canRecord = false;
        var AudioContext = window.AudioContext || window.webkitAudioContext; // shim for AudioContext when it's not avb. 
        var audioContext = new AudioContext; //new audio context to help us record
        var speechEvents;
        $(".next-button").prop("disabled", true).css('opacity', 0.5); //having the next button disabled at startup

        //variables needed for identifying silence
        var speechStopped = true;
        var silenceSeconds = 0;
        var silenceCount;
        var typeId = $("#typeId").val();

        var _url = window.location.href;
        var myArray = _url.match(/\d+/g);
        var examId = myArray[1];
        var itemId = myArray[2];

        var questionInterval = 3;

       

        switch (typeId) {
            case "read_aloud":
                answerDuration = questionDuration;
                break;
            case "repeat_sentence":
                answerDuration = 15;
                questionDuration += 3;
                break;
            case "describe_image":
                answerDuration = 40;
                break;
            case "retell_lecture":
                answerDuration = 40;
                break;
            case "answer_short_question":
                answerDuration = 10;
                questionDuration += 3;
                break;

        }
        interval = questionDuration + answerDuration;
        if (typeId == "retell_lecture") {
            interval += 10;
        }

        mid = answerDuration;

        //start main code block upon page load

        if (typeId == "repeat_sentence" || typeId == "retell_lecture" || typeId =="answer_short_question") {
            var countdown_question = setInterval(function () {
                $("#countdown_question").text("Status : Beginning in " + questionInterval + " seconds");
                if (questionInterval == 0) {
                    $("#countdown_question").text("Status : Playing");
                    var questionProgressCounter = 0;
                    var questionProgressRatio = 0;

                    var questionProgress = setInterval(function () {
                        questionProgressCounter++;
                        if (questionProgressCounter > questionDuration) {
                            $("#countdown_question").text("Status : Completed");
                            clearInterval(countdown_question);
                           
                        }
                        questionProgressRatio = (questionProgressCounter / questionDuration)*100;
                        $("#progressBarQuestion").css("width", questionProgressRatio + "%");  
                    }, 1000);


                    var audio = document.getElementById("myAudio");

                    const playPromise = audio.play();
                    if (playPromise !== null) {
                        playPromise.catch(() => { audio.play(); })
                    }
                    
                    answerCountDown();
                    clearInterval(countdown_question); // jump out of setInterval()
                }
                
                
                questionInterval--;


            }, 1000);

            //var audio = new Audio('/Questions/' + examId + "/Speaking/" + typeId + "/" + itemId + ".mp3");


            //var promise = document.getElementById("myAudio").play();

            //if (promise !== undefined) {
            //    promise.then(_ => {
            //        // Autoplay started!
            //        promise.play();
            //    }).catch(error => {
            //        // Autoplay was prevented.
            //        // Show a "Play" button so that user can start playback.
            //    });
            //}
        }
        else {
            answerCountDown();
        }

        //finish main code block upon page load

        


        function answerCountDown() {
            var countdown_answer = setInterval(
                function () {
                    $("#countdown_answer").text("Beginning in " + (interval - mid) + " seconds.");
                    interval--;

                    if (interval < mid) {
                        $(".next-button").prop("disabled", false).css('opacity', 1); //enable the next button
                        $("#countdown_answer").text("Recording");



                        progress = ((mid - interval) / mid) * 100;
                        $("#progressBarAnswer").css("width", progress + "%");

                        //start recording
                        if (canRecord == false) {
                            canRecord = true;
                            startRecording();
                        }

                    }
                    if (interval < 0) {

                        $("#countdown_answer").text("Completed");
                        clearInterval(countdown_answer); // jump out of setInterval()
                        stopRecording();
                        speechEvents.stop();
                    }
                },
                1000
            );
        }


        //}

        //start recording
        function startRecording() {

            var constraints = { audio: true, video: false }

            navigator.mediaDevices.getUserMedia(constraints).then(function (stream) {
                console.log("getUserMedia() success, stream created, initializing Recorder.js ...");

                /* assign to gumStream for later use */
                gumStream = stream;

                //start using hark.js to identify silence
                var options = {};
                speechEvents = hark(gumStream, options);

                //catching the speaking event
                speechEvents.on('speaking', function () {
                    speechStopped = false;
                    console.log('speaking');
                });

                //catching the stopped_speaking event
                speechEvents.on('stopped_speaking', function () {
                    console.log('stopped_speaking');
                    speechStopped = true;
                    silenceTimer();
                });
                //end using hark.js to identify silence

                /* use the stream */
                input = audioContext.createMediaStreamSource(stream);

                /* 
                Create the Recorder object and configure to record mono sound (1 channel)
                Recording 2 channels  will double the file size
                */
                rec = new Recorder(input, { numChannels: 1 });

                //start the recording process
                rec.record();
                silenceTimer();

                console.log("Recording started");

            }).catch(function (err) {
                console.log(err);
            });
        }


        //Identifying and timing silence
        function silenceTimer() {
            silenceCount = setInterval(function () {
                if (!speechStopped) {
                    console.log("The silence ended in under 3 seconds!");
                    silenceSeconds = -1;
                    clearInterval(silenceCount);
                }
                else if (speechStopped) {
                    silenceSeconds++;
                    if (silenceSeconds > 3) {
                        console.log("The silence lasted over 3 seconds!");
                        silenceSeconds = -1;

                        speechEvents.stop();
                        clearInterval(silenceCount);
                        stopRecording();
                    }
                }
            }, 1000);
        }

        //stop recording
        function stopRecording() {
            console.log("stopButton clicked");


            //tell the recorder to stop the recording
            rec.stop();

            //stop microphone access
            gumStream.getAudioTracks()[0].stop();

            //create the wav blob and pass it on to createDownloadLink
            rec.exportWAV(downloadRecording);

        }

        //download the recording
        function downloadRecording(blob) {
            var filename = new Date().toISOString(); //filename to send to server without extension

            var fd = new FormData();
            fd.append("audio_data", blob, filename);
            console.log("The file name is " + filename);

            //ajax call to controller to upload the file to server
            $.ajax({
                url: "/Speaking/Upload/" + examId + "/" + typeId +"/"+itemId,
                type: 'POST',
                data: fd,
                processData: false,  // tell jQuery not to process the data
                contentType: false,  // tell jQuery not to set contentType
                success: function (response) {
                    window.location = response.url;
                },
                error: function (jqXHR) {
                    alert("error ");
                },
                complete: function (jqXHR, status) {
                },
            });

            return false;
        }

        //when next button is clicked
        //$("#readaloud_next,#repeat_s_next,#describe_image_next,#retell_l_next,#answer_short_q_next").click
        //    (function () {
        //        stopRecording();
        //        speechEvents.stop();
        //    }
        //);

        $(".next-button").click
            (function () {
                stopRecording();
                speechEvents.stop();
            }
            );
    }
);