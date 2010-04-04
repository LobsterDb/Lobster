/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
 Lobster.Shell.Full = function() {
    this.initialize();
    this.addLobsterForm();
};

Lobster.Shell.Full.prototype = {

    initialize: function() {
        var viewport = new Ext.Viewport({
            layout: 'fit',
            items: [{
                xtype: 'tabpanel',
                id: 'tabs',
                items: []
            }]
        });
    },
    
    openForm: function(args){
        var form = Lobster.Forms.CompositeForm.CompositeForm.getNew({
            compositeModelId: args.compositeModelId,
            compositeModelSource: args.compositeModelSource,
            compositeKey: args.compositeKey,
            shell: this
        });
        this.getTabPanel().add(form._extPanelConfig);
    },
    
    addLobsterForm: function(){
        this.openForm({
            compositeModelId: '26761d27-392c-4466-ab5d-7849c7c91474',
            compositeModelSource: 'Framework'
        });
        this.getFormList();
  },
  
    getFormList: function(){
        var compositeSet = new Lobster.Composites.CompositeSet({
            compositeModelId: '26761d27-392c-4466-ab5d-7849c7c91474',
            compositeModelSource: 'Framework'
        });
        var list = compositeSet.getObjectPickerData();
        for (var i = 0; i < list.length; i++){
            console.log(list[i].Name);
            this.openForm({
                compositeModelId: list[i].ClassModelId,
                compositeModelSource: 'Application'
            });
        }
    },      
    
    getTabPanel: function(){
        return Ext.getCmp('tabs');
    }
    
};