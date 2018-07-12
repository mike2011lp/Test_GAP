app.functions.login = {};

//Create functions for Login
app.functions.login.HandleLoginSuccess = function (data) {
    window.location.href = data;
    return false;
};

//Handle ajax response
app.functions.login.HandleLoginFailure = function (xhr, status, message) {
    app.functions.common.HandleAjaxResponse(xhr, status, message);
};