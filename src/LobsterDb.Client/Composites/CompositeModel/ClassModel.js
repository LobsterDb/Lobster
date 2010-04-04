/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 * licensing@lobsterdb.com
 */
Lobster.Composites.ClassModelReader.ClassModel = function(args) {
    this._classModelSource = args.classModelSource;
    var serverProxy = new Lobster.Utilities.ServerProxy({
        className: 'ClassModelReader'
    });
    var response = serverProxy.executeRequest({
        methodName: 'GetState',
        arguments: {
            ClassModelId: args.classModelId,
            ClassModelSource: args.classModelSource
        }
    });
    if (response.executed && response.succeeded) {
        this._stateObject = response.returnObject;
    }
    response = serverProxy.executeRequest({
        methodName: 'GetListOfChildClassModels',
        arguments: {
            ClassModelId: args.classModelId,
            ClassModelSource: args.classModelSource
        }
    });
    if (response.executed && response.succeeded) {
        this._childClassModelsArray = response.returnObject;
    }
    this._template = null;
}

Lobster.Composites.ClassModelReader.ClassModel.classModelId = 
    '26761d27-392c-4466-ab5d-7849c7c91474';

Lobster.Composites.ClassModelReader.ClassModel.getInstance = function() {

}

Lobster.Composites.ClassModelReader.ClassModel.prototype = {
    
    getClassModelId: function(){
        return this._stateObject.ClassModelId;
    },
    
    getClassModelSource: function(){
        return this._classModelSource;
    },

    getKeyName: function(){
        return this.getName() + 'Id';
    },

    getName: function(){
        return this._stateObject.Name;
    },

    getIsComposite: function(){
        return this._stateObject.IsComposite;
    },

    getSetName: function(){
        if (this._stateObject.SetNameOverload == ''){
            return this.getName() + 's';
        } else {
            return this._stateObject.SetNameOverload;
        }
    },
    
    getChildClassModels: function() {
        var array = [];
        for (var i = 0; i < this._childClassModelsArray.length; i++) {
            var classModel = new Lobster.Composites.ClassModelReader.ClassModel({
                classModelId: this._childClassModelsArray[i],
                classModelSource: this._classModelSource
            });
            array.push(classModel);
        }
        return array;
    },

    getFieldModels: function() {
        return new Lobster.Composites.ClassModelReader.FieldModels({
            stateArray: this._stateObject.FieldModels
        })
    },

    getTemplate: function(){
        if (this._template === null){
            this._template = this.buildTemplate();
        }
        return this._template;
    },

    buildTemplate: function(){
        var jso = {};
        var fieldModels = this.getFieldModels().getAll();
        for (var x = 0; x < fieldModels.length; x++){
            var fieldModel = fieldModels[x];
            jso[fieldModel.getName()] = fieldModel.getDefaultValue();
        }
        if (this.getIsComposite() === true){
            var childClassModels = this.getChildClassModels();
            for (var i = 0; i < childClassModels.length; i++){
                jso[childClassModels[i].getSetName()] = [];
            }        
        }
        return jso;
    }

};

