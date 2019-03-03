$(function () {
    $(".slider")
        .slider(
        {
            value : 50
        }
        )
        .slider("pips",
        {
            first: "pip",
            last: "pip",
            step : 10
        });

});