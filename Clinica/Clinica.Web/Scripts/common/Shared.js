app.functions.common = {};

//Mostrar notificación en pantalla
app.functions.common.ShowGrowl = function (configuration, message) {
    $.gritter.add({
        title: configuration.title,
        text: message,
        sticky: configuration.sticky,
        time: configuration.time,
        class_name: configuration.className
    });
};

//Verificar respuesta para determinar si se debe resalizar alguna redirección
app.functions.common.VerifyResponseUrl = function (xhr) {
    var result = "";
    var parsedObject;

    if (xhr.responseJSON && xhr.responseJSON.isRedirect) {
        result = xhr.responseJSON.url;
    }
    else if (xhr.responseText) {
        try {
            parsedObject = JSON.parse(xhr.responseText)
            result = parsedObject.isRedirect ? parsedObject.url : "";
        }
        catch (e) {
            result = "";
        };
    };

    return result;
};

//Mostrar notificaciones ajax
app.functions.common.HandleAjaxNotifications = function (xhr, status, message) {
    var msg = message ? message : xhr.statusText;

    if (msg) {
        switch (xhr.status) {
            case app.errorData.statusOk:
                app.functions.common.ShowGrowl(app.messages.configuration.success, msg);
                break;
            case app.errorData.statusBadRequest:
            case app.errorData.statusNotAuthorized:
            case app.errorData.statusNotFound:
                app.functions.common.ShowGrowl(app.messages.configuration.warning, msg);
                break;
            case app.errorData.statusInternalServerError:
                app.functions.common.ShowGrowl(app.messages.configuration.critical, msg);
                break;
        }
    };
};

//Manejo de respuestas ajax
app.functions.common.HandleAjaxResponse = function (xhr, status, message) {
    var redirectUrl = app.functions.common.VerifyResponseUrl(xhr);

    if (redirectUrl) {
        window.location.href = redirectUrl;
    }
    else {
        //Mostrar notificacion
        app.functions.common.HandleAjaxNotifications(xhr, status, message);

        //Verify Spinner and Loading Div
        app.functions.common.VerifySpinners();
    };
};

//Verificar spinners activos
app.functions.common.VerifySpinners = function () {
    if ($.active === 1 || $.active === 0 || isError) {
        app.functions.common.HideSpinner();
    };
};

//Esconder spinner
app.functions.common.HideSpinner = function () {
    var $loadingDiv = $(".loading-div");
    $loadingDiv.hideSpinner();
};

//Mostrar spinner
app.functions.common.ShowSpinner = function (event) {
    var $loadingDiv = $(".loading-div");
    $loadingDiv.displaySpinner(event);
};

//Esconder spinner de un elemento
$.fn.hideSpinner = function () {
    var $that = $(this);
    $that.spin(false);
    $that.hide();
};

//Mostrar spinner en un elemento
$.fn.displaySpinner = function (event) {
    var $that = $(this);
    if (event === undefined || event.button === undefined || event.button === 0) {
        $that.show();
        $that.spin(app.spinOptions);
    }
};