/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Forms.FormModel.ControlModelsSet = function(stateArray) {
    this.stateArray = stateArray;
}

Lobster.Forms.FormModel.ControlModelsSet.prototype = {

    getAll: function() {
        var collection = [];
        for (var x = 0; x < this.stateArray.length; x++) {
            var controlModel = new Lobster.Forms.FormModel.ControlModel(this.stateArray[x]);
            collection.push(controlModel);
        }
        return collection;
    }


}