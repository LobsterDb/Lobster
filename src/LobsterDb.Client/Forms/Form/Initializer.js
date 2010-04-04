/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Forms.Form.Initializer = function() {
}

Lobster.Forms.Form.Initializer.prototype = {
    
    buildPanelConfig: function(formModel, form, controlProxies) {
        var config = {
            xtype: 'panel',
            layout: 'fit',
            title: form.getTitle(),
            items: [{
                xtype: 'grouptabpanel',
                tabWidth: 130,
                activeGroup: 0,
                items: []
            }]
        };
        config.items[0].items.push(
            this.buildFormGroup(formModel, form, controlProxies)
        );
        return config;
    },

        buildGroupTabPanelConfig: function() {
            var config = {
                xtype: 'grouptabpanel',
                tabWidth: 130,
                activeGroup: 0,
                items: []
            };
            config.items.push(this.buildFormGroup());
        },

        buildFormGroup: function(formModel, form, controlProxies) {
            var config = {
                items: [
                    this.getFormPage(formModel, form, controlProxies, 'Form'),
                    this.getFormPage(formModel, form, controlProxies, 'Page 1'),
                ]
            };
            var classModel = form._classModel;
            var childClassModels = classModel.getChildClassModels();
            for (var i = 0; i < childClassModels.length; i++) {
                config.items.push(this.getGridPage(childClassModels[i], form));
            }
            return config;
        },

        getGridPage: function(classModel, form) {
            //var extToolbar = new Lobster.Forms.Form.Toolbar().buildExtToolbar(form);
            var gridBinder = new Lobster.Forms.Form.GridBinder(form);
            var gridPanel = gridBinder.getGridPanel(classModel);
            var config = {
                xtype: 'panel',
                layout: 'fit',
                title: classModel.getSetName(),
                frame: 'true',
                items: [gridPanel]//,
                //tbar: extToolbar
            };
            form._gridBinders.push(gridBinder);
            return config;
        },

        getFormPage: function(formModel, form, controlProxies, title) {
            var controlConfigs = [];
            var extFormPanelIndex = form._extFormPanels.length;
            var controlModels = formModel.getControlModelsSet().getAll();
            for (var i = 0; i < controlModels.length; i++) {
                var controlProxy = controlProxies.getNew(
                controlModels[i], extFormPanelIndex
            );
                controlConfigs.push(controlProxy.getExtConfig());
            }
            var formPage = this.getFormPanel(controlConfigs, form, title);
            form._extFormPanels.push(formPage);
            return formPage;
        },

        getFormPanel: function(items, form, title) {
            //setting ItemId causes crash when navigating to group tab
            var config = {
                labelWidth: 150,
                frame: true,
                title: title,
                bodyStyle: 'padding:0px 0px 0',
                width: 350,
                defaults: { width: 230 },
                defaultType: 'textfield',
                items: items
            }
            var toolbar = this.getToolbar(form);
            if (toolbar){
                config.tbar = toolbar;
            }
            var formPanel = new Ext.FormPanel(config);
            return formPanel;
        },

        getToolbar: function(form){
            if (form._classModel.getIsComposite() == true){
                return new Lobster.Forms.Form.Toolbar().buildExtToolbar(form);
            }
        },

        getBlankPage: function(title) {
            var item = {
                title: title,
                style: 'padding: 10px;',
                html: 'Test Page'
            }
            return item;
        }

    };