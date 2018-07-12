app.functions.citas = {};

app.functions.citas.ViewControlsInit = function () {
    var $form = $("#frmSolicitarCita");

    app.functions.common.SetDateControls();

    //Active unobstrusive validation
    $form.removeData("validator");
    $form.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse($form);
};

app.functions.citas.SolicitarSuccess = function (result, textStatus, xhr) {
    var $form = $("#frmSolicitarCita");

    app.functions.common.HandleAjaxNotifications(xhr, xhr.status, xhr.statusText);

    $form.clearFormControls();
};