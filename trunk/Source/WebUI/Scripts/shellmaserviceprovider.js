/// <reference path="jquery.js" />
/// <reference path="serviceproxy.js" />

ShellmaServiceProvider = function() {
    this.proxy = new ServiceProxy("http://localhost:5030/shellma");

    this.ComputeHash = function(text) {
        return this.invoke("ComputeHash", text);
    };

    this.SignHash = function(hash) {
        return this.invoke("SignHash", hash);
    };

    this.ExportPublicKey = function() {
        return this.invoke("ExportPublicKey");
    };

    this.VerifySignature = function(signature, data, publicKey) {
        var request = {
            Signature: signature,
            Data: data,
            PublicKey: publicKey
        };
        return this.invoke("VerifySignature", request);
    };

    this.invoke = function(method, message) {
        var response = false;
        this.proxy.invoke(method, message, function(result) {
            response = result;
        }, this.ProcessError);
        return response;
    };

    this.ProcessError = function(xhr) {
        var reason = xhr.status + ": " + xhr.statusText;
        alert("I've got an error: " + reason);
    };
};