/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Forms.Form.Form = function(args) {

    this._compositeModelId = args.compositeModelId;
    this._compositeModelSource = args.compositeModelSource;
    this._composite = args.composite;

    /*
    this._formModel = new Lobster.Forms.FormModel.FormModel({
    key: args.formModelId
    });
    */

    this._classModel = new Lobster.Composites.ClassModelReader.ClassModel({
        classModelId: this._compositeModelId,
        classModelSource: this._compositeModelSource
    });

    this._formModel = Lobster.Forms.FormModel.FormModel.getObject({
        compositeModelId: args.compositeModelId,
        compositeModelSource: args.compositeModelSource
    });

    this._controlProxies = new Lobster.ControlProxies.Manager(this);
    this._gridBinders = [];
    this._extFormPanels = [];

    var initializer = new Lobster.Forms.Form.Initializer();
    this._extPanelConfig = initializer.buildPanelConfig(this._formModel, this, this._controlProxies);

    this._composite.setFieldValue('BooleanField', true);
    this._controlProxies.bindAll();
    this.refreshGridBinders();
}

Lobster.Forms.Form.Form.prototype = {

    buildObjectPickerGrid: function(){
        var classModel = this._classModel;
        var gridModel = {
            Label: classModel.getSetName(),
            KeyField: classModel.getKeyName(),
            Columns: []
        };
        var fieldModels = classModel.getFieldModels().getAll();
        for (var i = 0; i < fieldModels.length; i++){
            var fieldModel = fieldModels[i];
            gridModel.Columns.push({
                FieldName: fieldModel.getName(),
                Label: fieldModel.getName(),
                Width: 150
            })
        }
        return gridModel;
    },

    _formModel: null,

    _compositeModelId: null,

    _composite: null,

    getComposite: function() {
        return this._composite;
    },

    getControlProxies: function() {
        return this._controlProxies;
    },

    getExtFormPanel: function(index) {
        return this._extFormPanels[index];
    },

    getTitle: function(){
        return this._formModel.getFormTitle();
    },

    loadLobsterObject: function(key) {
        /*
        var lobsterObject = new LobsterObject({
        progId: 
        key: id
        });
        */
        for (var x = 0; x < this.mControlProxiesArray.length; x++) {
            this.mControlProxiesArray[x].bind(lobsterObject);
        }
    },

    loadNewObject: function() {
        openWindow(this);
    },

    refreshGridBinders: function(){
        for (var i = 0; i < this._gridBinders.length; i++){
            this._gridBinders[i].refresh();
        }
    }    

};
