/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 * licensing@lobsterdb.com
 */
Lobster.Composites.DataTypes.DataTypes = {
    
    Boolean: 1,
    
    Guid: 12,

    Number: 4,
    
    Text: 10,

    getDataType: function(dataTypeId){
        switch (dataTypeId){
            case this.Boolean:
                return Lobster.Composites.DataTypes.Boolean;
            case this.Guid:
                return Lobster.Composites.DataTypes.Guid;
            case this.Number:
                return Lobster.Composites.DataTypes.Number;
            case this.Text:
                return Lobster.Composites.DataTypes.Text;
            default:
                throw new Error('bad data type ' + dataTypeID);
        }
    }
    
};
   