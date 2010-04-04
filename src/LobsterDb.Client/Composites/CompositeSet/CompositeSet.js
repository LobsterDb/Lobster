/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 * licensing@lobsterdb.com
 */
Lobster.Composites.CompositeSet = function(args) {
    this._compositeModelId = args.compositeModelId;
    this._compositeModelSource = args.compositeModelSource;
    this._serverProxy = new Lobster.Utilities.ServerProxy({
        className: 'CompositeSet'
    });
};

Lobster.Composites.CompositeSet.prototype = {

    getObjectPickerData: function() {
        var response = this._serverProxy.executeRequest({
            methodName: 'GetObjectPickerData',
            arguments: {
                CompositeModelId: this._compositeModelId,
                CompositeModelSource: this._compositeModelSource
            }
        });
        if (response.executed && response.succeeded){
            return response.returnObject;
        }

    }
};