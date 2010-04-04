/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.ControlProxies.ScalarControl = function(args) {
    this._extFormPanelIndex = args.extFormPanelIndex;
    this._extItemId = args.extItemId;
    this.mConfig = args;
    //this.bind();
}

Lobster.ControlProxies.ScalarControl.prototype = {

    getControlModel: function() {
        return this.mConfig.controlModel;
    },

    getComposite: function() {
        return this.mConfig.parent.getComposite();
    },

    getExtItemId: function() {
        return this._extItemId;
    },

    getExtConfig: function() {
        //overload in subclasses
    },

    getExtControl: function() {
        return this.mConfig.parent.getExtFormPanel(this._extFormPanelIndex).getComponent(this._extItemId);
    },

    handleChange: function(newValue) {
        this.getComposite().setFieldValue(
            this.getControlModel().getFieldName(), newValue
        );
        this.mConfig.parent.refreshAll();
    },

    setControlValue: function() {
        var extControl = this.getExtControl();
        var fieldName = this.getControlModel().getFieldName();
        var controlValue = this.getComposite().getFieldValue(fieldName);
        extControl.setValue(controlValue);
    },

    bind: function() {
        this.mLobsterObject = this.mConfig.parent.getComposite();
        var extControl = this.getExtControl();
        extControl.on('change',
            function(field, newValue, oldValue) {
                this.handleChange(newValue)
            },
            this
        );
        this.setControlValue();
    }

};
