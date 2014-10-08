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