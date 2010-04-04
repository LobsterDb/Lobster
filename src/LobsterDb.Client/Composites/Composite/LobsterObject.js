/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Composites.Composite.LobsterObject = function(){

}

Lobster.Composites.Composite.LobsterObject.prototype = {

    getFieldValue: function(fieldName) {
        //throw exception if not a field
        return this._stateGraph[fieldName];
    },

    setFieldValue: function(fieldName, newValue) {
        this._stateGraph[fieldName] = newValue;
    },
    
    getJson: function() {
        return JSON.stringify(this._stateGraph);
    }
    
};