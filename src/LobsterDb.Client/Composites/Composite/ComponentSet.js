/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Composites.Composite.ComponentSet = function(args){
    this._stateArray = args.stateArray;
    this._classModel = args.classModel;
    this._composite = args.composite;
}

Lobster.Composites.Composite.ComponentSet.prototype = {

    item: function(key){
        for (var i = 0; i < this._stateArray.length; i++){
            if (this._stateArray[i][this._classModel.getKeyName()] == key){
                return new Lobster.Composites.Composite.Component({
                    state: this._stateArray[i]
                });
            }
        }    
    },
    
    getNew: function(){
        var classModel = this._classModel;
        var state = classModel.getTemplate();
        state[classModel.getKeyName()] = 
            this._composite.getComponentKey(classModel.getClassModelId);
        this._stateArray.push(state);
        return new Lobster.Composites.Composite.Component({
            state: state
        });
    },
    
    getAll: function(){
        var all = [];
        for (var i = 0; i < this._stateArray.length; i++){
            //all.push(
            //    new Lobster.Composites.Composite.
        }
    }

};