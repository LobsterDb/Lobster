/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Forms.FormModel.FormModel = function(args) {
    if (args.objectState) {
        this.stateGraph = args.objectState;
    } else {

    }
    /*
    if (args.key && args.key != 0) {
    this.loadStateGraph(args.key);
    } else {
    this.mFieldValues = this.getTemplate();
    }
    */
}

Lobster.Forms.FormModel.FormModel.getObject = function(args) {

    var serverProxy = new Lobster.Utilities.ServerProxy({
        className: 'ClassModel'
    });

    var response = serverProxy.executeRequest({
        methodName: 'BuildFormModel',
        arguments: {
            CompositeModelId: args.compositeModelId,
            CompositeModelSource: args.compositeModelSource
        }
    });

    if (response.executed && response.succeeded) {
        var formModel = new Lobster.Forms.FormModel.FormModel({
            objectState: response.returnObject
        });
        return formModel;
    }

}

Lobster.Forms.FormModel.FormModel.prototype = {

    stateGraph: {},

    loadGraphState1: function() {
        var sp = new Lobster.ServerProxy({
            serverUrl: "/Sandbox/People.aspx",
            methodName: "GetGraphState",
            methodParameters: { 'personId': 2 }
        });
        var result = sp.executeRequest();
        this.mFieldValues = result.serverResponse.Response;
        window.alert(this.getJson());
    },

    getFormTitle: function() {
        return this.stateGraph['FormTitle'];
    },

    getControlModelsSet: function() {
        var x = new Lobster.Forms.FormModel.ControlModelsSet(this.stateGraph['ControlModels']);
        return x;
    }

};