/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Controls.Grids.ReadOnly = function(args) {
    this._config = args.config || {};
    this._jsonData = args.jsonData;
    this._gridModel = args.gridModel;
}

Lobster.Controls.Grids.ReadOnly.prototype = {

    getGridPanel: function() {
        var columnModel = new Ext.grid.ColumnModel({
            columns: this.getExtColumns()
        });
        var dataStore = this.getJsonStore();
        var data = this._jsonData;
        dataStore.loadData({ rows: data });
        var config = this._config;
        config.ds = dataStore;
        config.cm = columnModel;
        config.height = 350;
        var extGrid = new Ext.grid.GridPanel(config);
        return extGrid;
    },

    getExtColumns: function() {
        var modelColumns = this._gridModel.Columns;
        var extColumns = [];
        for (var x = 0; x < modelColumns.length; x++) {
            var modelColumn = modelColumns[x];
            extColumns.push({
                header: modelColumn.Label,
                width: modelColumn.Width,
                dataIndex: modelColumn.FieldName,
                sortable: true,
                tooltip: 'add support'
            });
        };
        return extColumns;
    },

    getJsonStore: function() {
        var extFields = [];
        extFields.push({
            name: this._gridModel.KeyField
        });
        var modelColumns = this._gridModel.Columns;
        for (var x = 0; x < modelColumns.length; x++) {
            extFields.push({
                name: modelColumns[x].FieldName
            });
        }
        var store = new Ext.data.JsonStore({
            idProperty: this._gridModel.KeyField,
            root: 'rows',
            fields: extFields
        });
        return store;
    },

    getDataStore1: function() {
        var extFields = [];
 
        extFields.push({
            name: this.getModelView().KeyField
        });
        
        var modelColumns = this.getModelView().Columns;
        
        for (var x = 0; x < modelColumns.length; x++) {
            extFields.push({
                name: modelColumns[x].FieldName
            });
        }
        
        var store = new Ext.data.ArrayStore({
            fields: extFields
        });
        
        return store;
    
    }
    
};