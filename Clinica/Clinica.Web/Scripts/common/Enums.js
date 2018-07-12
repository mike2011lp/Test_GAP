var app = {};

// Opciones globales para spinner
app.spinOptions = {
    lines: 15, // The number of lines to draw
    length: 28, // The length of each line
    width: 14, // The line thickness
    radius: 42, // The radius of the inner circle
    scale: 1, // Scales overall size of the spinner
    corners: 1, // Corner roundness (0..1)
    color: "#6B4584", // #rgb or #rrggbb or array of colors
    opacity: 0.25, // Opacity of the lines
    rotate: 0, // The rotation offset
    direction: 1, // 1: clockwise, -1: counterclockwise
    speed: 1, // Rounds per second
    trail: 60, // Afterglow percentage
    fps: 20, // Frames per second when using setTimeout() as a fallback for CSS
    zIndex: 2e9, // The z-index (defaults to 2000000000)
    className: "spinner", // The CSS class to assign to the spinner
    top: "50%", // Top position relative to parent
    left: "50%", // Left position relative to parent
    shadow: false, // Whether to render a shadow
    hwaccel: false, // Whether to use hardware acceleration
    position: "absolute" // Element positioning
};

//Códigos de respuesta
app.errorData = {
    statusOk: 200,
    statusBadRequest: 400,
    statusNotAuthorized: 401,
    statusNotFound: 404,
    statusInternalServerError: 500
};

//Mensajes
app.messages = [];

//Configuración de mensajes
app.messages.configuration = {
    critical: {
        title: "Critical",
        className: "growl-critical",
        time: 3000,
        sticky: false
    },
    warningSticky: {
        title: "Warning",
        className: "growl-warning",
        sticky: true
    },
    warning: {
        title: "Warning",
        className: "growl-warning",
        time: 3000,
        sticky: false
    },
    information: {
        title: "Information",
        className: "growl-information",
        time: 3000,
        sticky: false
    },
    success: {
        title: "Success",
        className: "growl-success",
        time: 3000,
        sticky: false
    }
};

//Datos por defecto
app.defaults = {
    dateFormat: "MM/DD/YYYY",
    timeFormat: "LT",
    dateTimeFormat: "MM/DD/YYYY LT",
    locale: "en"
};

//Formatos de fechas
app.dateFormat = {
    dateTimeTwentyFourHour: "MM/DD/YYYY HH:mm",
    dateTimeTwentyFourHourWithSeconds: "MM/DD/YYYY HH:mm:ss",
    dateMonthYearFull: "MM/YYYY",
    dateMonthYearShort: "MM/YY",
    dateMonthMinNameYearFull: "MMM/YYYY"
};

app.parameters = {
    tokenTextName: "__RequestVerificationToken"
};

//Espacio para funciones
app.functions = [];

//Espacio para elementos estáticos en pantalla
app.staticElements = [];