/// <reference path="jquery.js" />

ServiceProxy = function(serviceUrl) {
    this.serviceUrl = serviceUrl;
    this.async = false;
    
    this.invoke = function (method, message, onSuccess, onError) {
        $.ajax({
            url: this.serviceUrl + "/" + method,
            crossDomain: true,
            data: this.stringify(message),
            type: "POST",
            processData: false,
            contentType: "application/json",
            timeout: 10000,
            async: this.async,
            dataType: "text",  
            success: function (result) {
                var isVoid = result == "";
                var response = isVoid ? true : JSON.parse(result);
                if (onSuccess) {
                    onSuccess(response);
                }
            },
            error: function (xhr, status) {
                if (onError) {
                    onError(xhr, status);
                }
            },
        });
    };
    
    this.stringify = function (json) {
        // http://www.west-wind.com/weblog/posts/2009/Sep/15/Making-jQuery-calls-to-WCFASMX-with-a-ServiceProxy-Client
        var reIso = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)Z$/;
        /// <summary>
        /// Wcf specific stringify that encodes dates in the
        /// a WCF compatible format ("/Date(9991231231)/")
        /// Note: this format works ONLY with WCF. 
        ///       ASMX can use ISO dates as of .NET 3.5 SP1
        /// </summary>
        /// <param name="key" type="var">property name</param>
        /// <param name="value" type="var">value of the property</param>         
        return JSON.stringify(json, function(key, value) {
            if (typeof value == "string") {
                var a = reIso.exec(value);
                if (a) {
                    var val = '/Date(' + new Date(Date.UTC(+a[1], +a[2] - 1, +a[3], +a[4], +a[5], +a[6])).getTime() + ')/';
                    this[key] = val;
                    return val;
                }
            }
            return value;
        });
    };
};