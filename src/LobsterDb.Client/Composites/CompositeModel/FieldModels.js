/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 * licensing@lobsterdb.com
 */
Lobster.Composites.ClassModelReader.FieldModels = function(args){
    this._stateArray = args.stateArray;
}

Lobster.Composites.ClassModelReader.FieldModels.prototype = {

    item: function(key){
    
    },
    
    getAll: function(){
        var all = [];
        for (var i = 0; i < this._stateArray.length; i++){
            all.push(
                new Lobster.Composites.ClassModelReader.FieldModel({
                    state: this._stateArray[i]
                })
            );
        }
        return all;
    }

};