/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Forms.CompositeForm.CompositeForm = function(args){
    Lobster.Forms.CompositeForm.CompositeForm.superclass.call(this, args);
    this._shell = args.shell;
}

Lobster.Forms.CompositeForm.CompositeForm.getNew = function(args){
    args.composite = new Lobster.Composites.Composite.Composite({
        compositeModelId: args.compositeModelId,
        compositeModelSource: args.compositeModelSource,
        key: args.compositeKey
    });
    return new Lobster.Forms.CompositeForm.CompositeForm(args);
}

Lobster.Utilities.Utilities.extend(Lobster.Forms.CompositeForm.CompositeForm,
    Lobster.Forms.Form.Form, {
   
    onToolbarNew: function() {
        this._composite = new Lobster.Composites.Composite.Composite({
            compositeModelId: this._compositeModelId,
            compositeModelSource: this._compositeModelSource
        });
        this._controlProxies.refreshAll();
        this.refreshGridBinders();
    },

    onToolbarOpen: function() {
        var gridModel = this.buildObjectPickerGrid();
        var form = this;
        var x = new Lobster.Controls.ObjectPicker.getNew({
            compositeModelId: this._compositeModelId,
            compositeModelSource: this._compositeModelSource,
            keyName: this._classModel.getKeyName(),
            gridModel: gridModel,
            selectHandler:  
            function selectHandler(key) {
                form.handleOpen(key);
            }
        });
    },

    handleOpen: function(key) {
        this._composite = new Lobster.Composites.Composite.Composite({
            compositeModelId: this._compositeModelId,
            compositeModelSource: this._compositeModelSource,
            key: key
        });
        this._controlProxies.refreshAll();
        this.refreshGridBinders();
    },
    
    onToolbarSave: function() {
        var key = this._composite.persist();
        if (this._classModel.getClassModelId() == '26761d27-392c-4466-ab5d-7849c7c91474'){
            this._shell.openForm({
                compositeModelId: key,
                compositeModelSource: 'Application'
            });
        }
    }
    
});