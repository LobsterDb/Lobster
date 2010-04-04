/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Controls.ObjectPicker = function(args) {
    //this._compositeModelReader = args.compositeModelReader;
    this._keyName = args.keyName;
    this._extGrid = args.extGrid;
    this._selectHandler = args.selectHandler;
    this.initLayout(this._extGrid);
}

Lobster.Controls.ObjectPicker.getNew = function(args) {

    var compositeSet = new Lobster.Composites.CompositeSet({
        compositeModelId: args.compositeModelId,
        compositeModelSource: args.compositeModelSource
    });
    
    var data = compositeSet.getObjectPickerData();

    var grid = new Lobster.Controls.Grids.ReadOnly({
        jsonData: data,
        gridModel: args.gridModel
    });

    var extGrid = grid.getGridPanel();

    var objectPicker = new Lobster.Controls.ObjectPicker({
        extGrid: extGrid,
        keyName: args.keyName,
        selectHandler: args.selectHandler
    });

    return objectPicker;

}

Lobster.Controls.ObjectPicker.prototype = {

    initLayout: function(extGrid) {
        extGrid.on('rowdblclick', this.fireSelect, this);
        this.extWindow = new Ext.Window({
            title: 'Open',
            closable: true,
            closeAction: 'hide',
            width: 600,
            height: 350,
            plain: true,
            //layout: 'border',
            buttons: this.buildButtons(),
            items: extGrid
        });
        this.extWindow.show();
    },

    buildButtons: function() {
        var ok = new Ext.Button({
            text: 'OK'
        });
        ok.on('click', this.fireSelect, this);
        var cancel = new Ext.Button({
            text: 'Cancel' 
        });
        cancel.on('click', this.hide, this);
        return [ok, cancel];
    },

    hide: function() {
        this.extWindow.hide();
    },

    fireSelect: function() {
        var k = this._extGrid.getSelectionModel().
            getSelected().get(this._keyName);
        this.hide();
        this._selectHandler(k);
    }

};
