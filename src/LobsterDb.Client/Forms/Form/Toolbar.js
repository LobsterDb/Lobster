/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Forms.Form.Toolbar = function(){

}

Lobster.Forms.Form.Toolbar.prototype = {

    buildExtToolbar: function(form) {

        var iconFolder = '/Lobster/Icons/';

        var toolbar = new Ext.Toolbar('toolbar');

        toolbar.addButton({
            text: 'New',
            cls: 'x-btn-text-icon new-btn',
            icon: iconFolder + 'file.ico',
            handler: function() {
                form.onToolbarNew();
            }
        });

        toolbar.addButton({
            text: 'Open',
            cls: 'x-btn-text-icon open-btn',
            icon: iconFolder + 'folder.ico',
            handler: function() {
                form.onToolbarOpen();
            }

        });

        toolbar.addButton({
            text: 'Save',
            icon: iconFolder + 'floppy.ico',
            cls: 'x-btn-text-icon disk-btn',
            handler: function() {
                form.onToolbarSave();
            }
        });

        return toolbar;
    }

};