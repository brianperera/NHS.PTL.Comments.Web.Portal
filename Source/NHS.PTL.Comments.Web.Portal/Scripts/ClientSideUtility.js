$(document).ready(function () {
    $(".navigationBarItems").show();
});

function pageLoad() {

    $("#SearchBoxContent_patientTextbox").hide();
    $("#txtWaterMark").show();

    $("#SearchBoxContent_patientTextbox").blur(function () {

        if ($(this).val() == "") {
            $("#SearchBoxContent_patientTextbox").hide();
            $("#txtWaterMark").show();
        }
    });

    $("#txtWaterMark").focus(function () {
        $(this).hide();
        $("#SearchBoxContent_patientTextbox").show();
        $("#SearchBoxContent_patientTextbox").focus();
    });
}

function print() {
    window.print();
}

$(function () {
    var zIndexNumber = 1000;
    $('div').each(function () {
        $(this).css('zIndex', zIndexNumber);
        zIndexNumber -= 10;
    });
});