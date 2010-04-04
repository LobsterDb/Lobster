/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.ControlProxies.Manager = function(form) {
    this._form = form;
    this._collection = [];
}

Lobster.ControlProxies.Manager.prototype = {

    _collection: null,

    add: function(item) {
        this._collection.push(item);
    },

    getExtFormPanel: function(index) {
        return this._form.getExtFormPanel(index);
    },

    getComposite: function() {
        return this._form.getComposite();
    },

    bindAll: function() {
        for (var i = 0; i < this._collection.length; i++) {
            this._collection[i].bind();
        }
    },

    refreshAll: function(){
        for (var i = 0; i < this._collection.length; i++) {
            this._collection[i].setControlValue();
        }
    },
    
    getNew: function(controlModel, extFormPanelIndex) {

        var controlTypeName = controlModel.getControlTypeName();

        var controlProxy = new Lobster.ControlProxies[controlTypeName]({
            controlModel: controlModel,
            parent: this,
            extFormPanelIndex: extFormPanelIndex,
            extItemId: this._collection.length
        })

        this.add(controlProxy);

        return controlProxy;

    }

};