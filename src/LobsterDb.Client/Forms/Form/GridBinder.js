/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Forms.Form.GridBinder = function(form) {
    this._form = form;
}

Lobster.Forms.Form.GridBinder.prototype = {

    buildGridModel: function(classModel) {
        var gridModel = {
            Label: classModel.getSetName(),
            KeyField: classModel.getKeyName(),
            Columns: []
        };
        var fieldModels = classModel.getFieldModels().getAll();
        for (var i = 0; i < fieldModels.length; i++) {
            var fieldModel = fieldModels[i];
            gridModel.Columns.push({
                FieldName: fieldModel.getName(),
                Label: fieldModel.getName(),
                Width: 150
            });
        }
        this._classModel = classModel;
        return gridModel;
    },
    
    buildToolbar: function(){
        var iconFolder = '/Lobster/Icons/';
        var toolbar = new Ext.Toolbar('toolbar');
        var me = this;
        toolbar.addButton({
            text: 'New',
            cls: 'x-btn-text-icon new-btn',
            icon: iconFolder + 'file.ico',
            handler: function() {
                me.onNew();
            }
        });
        return toolbar;
    },
    
    getGridPanel: function(classModel) {
        var grid = new Lobster.Controls.Grids.ReadOnly({
            jsonData: [],
            gridModel: this.buildGridModel(classModel),
            config: {
                tbar: this.buildToolbar()
            }
        });
        var gridPanel = grid.getGridPanel();
        gridPanel.on('rowdblclick', this.onSelect, this);
        this._gridPanel = gridPanel;
        return gridPanel;
    },
    
    openComponentForm: function(component){
        var form = Lobster.Forms.ComponentForm.ComponentForm.getNew({
            compositeModelId: this._classModel.getClassModelId(),
            compositeModelSource: this._classModel.getClassModelSource(),
            component: component
        });
        this.extWindow = new Ext.Window({
            title: this._classModel.getName(),
            closable: true,
            closeAction: 'hide',
            width: 800,
            height: 600,
            modal: true,
            plain: true,
            layout: 'fit',
            //buttons: this.buildButtons(),
            items: form._extPanelConfig
        });
        this.extWindow.on('hide', this.onClose, this);
        this.extWindow.show();
    },
    
    onClose: function(){
        this.refresh();
    },
    
    getComponentSet: function(){
        return this._form.getComposite().getChildSet(
            this._classModel.getClassModelId()
        );
    },
    
    onNew: function(){
        this.openComponentForm(this.getComponentSet().getNew());
    },
    
    onSelect: function(){
        var key = this._gridPanel.getSelectionModel().
            getSelected().get(this._classModel.getKeyName());
        var component = this._form.getComposite().getChildSet(
            this._classModel.getClassModelId()
        ).item(key);
        this.openComponentForm(component);
    },
    
    refresh: function(){
        var setName = this._classModel.getSetName();
        var composite = this._form.getComposite();
        var dataArray = composite._stateGraph[setName];
        this._gridPanel.store.loadData({ rows: dataArray }, false);
    }

};