/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Utilities.ServerProxy = function(config) {
    //this.serverUrl = config.serverUrl;
    this.serverUrl = '/Lobster/Utilities/ServerProxy.aspx';
    this.className = config.className;
    this.sessionContext = config.sessionContext;
}

Lobster.Utilities.ServerProxy.prototype = {

    buildPostString: function() {
        var requestObject = {
            ClassName: this.className,
            MethodName: this.methodName,
            Arguments: this.arguments
        }
        var json = JSON.stringify(requestObject);
        //need to encode or & in parameter names will be
        //treated as parameter break
        json = encodeURIComponent(json);
        return 'Request=' + json;
        return postString;
    },

    executeRequest: function(arguments) {
        this.methodName = arguments.methodName;
        this.arguments = arguments.arguments;
        this.returnType = arguments.returnType;
        //synchronous
        var postString = this.buildPostString();
        var xhr = new XMLHttpRequest();
        xhr.open("POST", this.serverUrl, false);
        xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");  //needed for IIS to parse body
        xhr.setRequestHeader("Content-Length", "length");
        xhr.send(postString);
        if (xhr.status == 200) {
            var returnObject = eval('(' + xhr.responseText + ')');
            response = {
                xhr: xhr,
                executed: true,
                succeeded: true,
                returnObject: returnObject.ReturnValue
            }
        } else {
            returnWrapper.callExecuted = false;

            console.log('Ajax call failed: launching error details window');
            //window.open('ErrorDetails.aspx?html=' +
            //        encodeURIComponent(xhr.responseText));
            console.log('Ajax call failed: debugger;');
            debugger;
            throw new Error('Ajax call failed');
        }
        return response;
    }
}