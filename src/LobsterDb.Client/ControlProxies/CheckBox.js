/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.ControlProxies.CheckBox = function(args) {
    Lobster.ControlProxies.CheckBox.superclass.call(this, args);
};

Lobster.Utilities.Utilities.extend(Lobster.ControlProxies.CheckBox,
    Lobster.ControlProxies.ScalarControl, {

    getExtConfig: function() {

        var controlModel = this.getControlModel();
    
        var config = {
            fieldLabel: controlModel.getControlLabel(),
            name: controlModel.getFieldName(),
            itemId: this._extItemId,
            xtype: 'checkbox'
        };

        return config;
    
    }

});