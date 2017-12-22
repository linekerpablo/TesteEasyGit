function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));

    var month = dt.getMonth() + 1;

    if (window.location.href.indexOf('localhost') > -1) {
        var day = dt.getDate();
    } else {
        var day = dt.getDate() + 1;
    }

    return ("00" + day).slice(-2) + "/" + (("00" + month).slice(-2)) + "/" + dt.getFullYear();
}