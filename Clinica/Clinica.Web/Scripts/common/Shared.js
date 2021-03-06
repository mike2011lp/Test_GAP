﻿app.functions.common = {};

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

app.functions.common.SetDateControls = function (dateSelector, pConfiguration) {
    var defaultDateSelector = $(".date");
    var resultDateSelector = dateSelector ? dateSelector : (defaultDateSelector ? defaultDateSelector : null);

    //Default config
    var config = {
        locale: app.defaults.locale,
        format: app.defaults.dateFormat,
        ignoreReadonly: true,
        allowInputToggle: true,
        useCurrent: false
    };

    //Merge with parameter config if it's sent
    if (pConfiguration) {
        $.extend(config, pConfiguration);
    };

    if (resultDateSelector !== null && resultDateSelector.length > 0) {
        resultDateSelector.datetimepicker(config);
    };
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

//Clear form data controls content
//notCleaningFields: Fields that will not be reset on this process
$.fn.clearFormControls = function (notCleaningFields) {
    var $form = $(this);

    //First check if its form
    if ($form.is("form")) {
        //Clear Data
        $form.find("input:text:not([data-no-form-reset]), input:password:not([data-no-form-reset])," +
            "input[type=number]:not([data-no-form-reset]), input[type=hidden]:not(input[name='__RequestVerificationToken']):not([data-no-form-reset]), textarea:not([data-no-form-reset])").not(notCleaningFields).val("");
        $form.find("input:radio, input:checkbox").not(notCleaningFields).prop("checked", false);

        //For select must verify if it has an option for empty values, else select the first one
        $form.find("select:not([data-no-form-reset])").not(notCleaningFields).each(function () {
            var $select = $(this),
                $emptyOption = $select.find("option[value='']");

            if ($emptyOption && $emptyOption.length) {
                $select.val("");
            }
            else {
                $select.val($select.find("option:first").val());
            }
        });

        //Refresh Multiple Select
        var $multipleControls = $form.find("select.multiple-select:not([data-no-form-reset])").not(notCleaningFields);

        if ($multipleControls && $multipleControls.length) {
            $multipleControls.multipleSelect("refresh");
        }
    }
};