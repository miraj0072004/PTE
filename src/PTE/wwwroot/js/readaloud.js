$(
    function () {

        //variable initialization
        var interval = 40; //total time
        var mid = 35; // point at which user recording starts
        var progress = 0;        
        URL = window.URL || window.webkitURL; //webkitURL is deprecated but nevertheless
        var gumStream; //stream from getUserMedia()
        var rec; //Recorder.js object
        var input; //MediaStreamAudioSourceNode we'll be recording
        var canRecord = false;        
        var AudioContext = window.AudioContext || window.webkitAudioContext; // shim for AudioContext when it's not avb. 
        var audioContext = new AudioContext; //new audio context to help us record
        var speechEvents;
        $("#readaloud_next").prop("disabled", true).css('opacity', 0.5); //having the next button disabled at startup

        //variables needed for identifying silence
        var speechStopped = true;
        var silenceSeconds = 0;
        var silenceCount;
        
        
        var countdown_ra = setInterval(
                function () {
                    $("#countdown_ra").text("Beginning in " + (interval - mid) + " seconds.");
                    interval--;

                    if (interval < mid )
                    {
                        $("#readaloud_next").prop("disabled", false).css('opacity', 1); //enable the next button
                        $("#countdown_ra").text("Recording");
                        progress = ((mid - interval) / mid) * 100;
                        $(".progress-bar").css("width", progress + "%"); 
                        
                        //start recording
                        if (canRecord == false) {
                            canRecord = true;
                            startRecording();
                        }
                        
                    }
                    if (interval < 0) {

                        $("#countdown_ra").text("Completed");
                        clearInterval(countdown_ra); // jump out of setInterval()
                        stopRecording();
                        speechEvents.stop();
                    }
                },
                1000
            );
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
                        
            var _url = window.location.href;
            var myArray = _url.match(/\d+/g);
            var examId = myArray[1];
            var readAloudId = myArray[2];

            var fd = new FormData();
            fd.append("audio_data", blob, filename);
            console.log("The file name is " + filename);

            //ajax call to controller to upload the file to server
            $.ajax({
                url: "/Speaking/Upload/" + examId + "/" + readAloudId,
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
        $("#readaloud_next").click
            ( function()
                {
                    stopRecording();
                    speechEvents.stop();
                }
            );
    }
);