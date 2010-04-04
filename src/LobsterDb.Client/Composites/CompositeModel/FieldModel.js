/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 * licensing@lobsterdb.com
 */
Lobster.Composites.ClassModelReader.FieldModel = function(args){
    this._state = args.state;
}

Lobster.Composites.ClassModelReader.FieldModel.prototype = {

    getDataTypeId: function(){
        return this._state.DataTypeId;
    },

    getDataType: function(){
        return Lobster.Composites.DataTypes.DataTypes.getDataType(
            this.getDataTypeId()
        );
    },

    getName: function(){
        return this._state.Name;
    },

    getDefaultValue: function(){
        return this.getDataType().defaultValue;
    }
    
}