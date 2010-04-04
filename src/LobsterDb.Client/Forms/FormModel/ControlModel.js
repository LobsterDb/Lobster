/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Forms.FormModel.ControlModel = function(stateObject) {
    this.stateObject = stateObject;
}

Lobster.Forms.FormModel.ControlModel.prototype = {

    getControlTypeName: function() {
        return this.stateObject['ControlTypeName'];
    },

    getFieldName: function() {
        return this.stateObject['FieldName'];
    },

    getControlLabel: function() {
        return this.stateObject['ControlLabel'];
    }


}