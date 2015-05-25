/// <reference path="jquery.js" />
/// <reference path="serviceproxy.js" />

ShellmaServiceProvider = function() {
    this.proxy = new ServiceProxy("http://localhost:5030/shellma");

    this.ComputeHash = function(text) {
        return this.invoke("ComputeHash", text);
    };

    this.ConvertToHex = function (data) {
        if (data == 'false') return null;
        return this.invoke("ConvertToHex", data);
    };

    this.SignHash = function(hash) {
        return this.invoke("SignHash", hash);
    };

    this.ExportPublicKey = function() {
        return this.invoke("ExportPublicKey");
    };

    this.ExportCertificate = function () {
        return this.invoke("ExportCertificate");
    };

    this.VerifySignature = function(signature, data, publicKey) {
        var request = {
            Signature: signature,
            Data: data,
            PublicKey: publicKey
        };
        return this.invoke("VerifySignature", request);
    };

    this.VerifyCertificate = function(signature, data, certificate) {
        var request = {
            Signature: signature,
            Data: data,
            Certificate: certificate
        };
        return this.invoke("VerifyCertificate", request);
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