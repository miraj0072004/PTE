$(function () {
    $(".slider")
        .slider(
        {
            min: 1,
            max: 100,
            value: 50,
            slide: function (event, ui) {
                document.getElementById("myAudio").volume = (ui.value / 100);
                //$("#test").text("The slider value is " + ui.value/100);
            }
        }
        )
        .slider("pips",
        {
            first: "pip",
            last: "pip",
            step : 10
        });

    //var slider = document.getElementById("volumeControl");
    ////volume control
    //slider.oninput = function () {
    //    document.getElementById("myAudio").value = this.value/100;
    //}
});