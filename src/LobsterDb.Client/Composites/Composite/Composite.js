/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Composites.Composite.Composite = function(config) {
    var classModel = new Lobster.Composites.ClassModelReader.ClassModel({
        classModelId: config.compositeModelId,
        classModelSource: config.compositeModelSource
    });
    //Lobster.Composites.Composite.Composite.superclass.call
    this.compositeModelId = config.compositeModelId;
    this._compositeModelSource = config.compositeModelSource;
    this.serverProxy = new Lobster.Utilities.ServerProxy({
        className: 'Composite'
    });

    if (config.key === undefined || config.key == 0) {
        //this._fieldValues = this.getTemplate();
        this._editMode = 'new'
        this._stateGraph = classModel.getTemplate();
    } else {
        this.key = config.key;
        this.loadGraphState();
    }

}

Lobster.Utilities.Utilities.extend(Lobster.Composites.Composite.Composite,
    Lobster.Composites.Composite.LobsterObject, {
    
    compositeModelId: '',

    editMode: null,

    serverProxy: null,

    _stateGraph: {},

    _componentKeyCounter: {},

    commit: function() {
        var json = this.getJson();
        console.log('commit: ' + json);
    },
    
    getComponentKey: function(componentModelId){
        if (this._componentKeyCounter[componentModelId] === undefined){
            this._componentKeyCounter[componentModelId] = -1;
        }
        return this._componentKeyCounter[componentModelId]--;
    },
    
    getChildSet: function(childClassModelId){
        var componentModel = new Lobster.Composites.ClassModelReader.ClassModel({
            classModelId: childClassModelId,
            classModelSource: this._compositeModelSource
        });
        var componentSetStateArray = this._stateGraph[componentModel.getSetName()];
        var componentSet = new Lobster.Composites.Composite.ComponentSet({
            stateArray: componentSetStateArray,
            classModel: componentModel,
            composite: this
        });
        return componentSet;
    },

    loadGraphState: function(key) {
        var response = this.serverProxy.executeRequest({
            methodName: 'GetState',
            arguments: {
                CompositeModelId: this.compositeModelId,
                CompositeModelSource: this._compositeModelSource,
                Key: this.key
            }
        });
        if (response.executed) {
            if (response.succeeded){ 
                this._stateGraph = response.returnObject;
            }
        }
    },

    persist: function() {
        var serverProxy = new Lobster.Utilities.ServerProxy({
            className: 'Composite'
        });
        var response = serverProxy.executeRequest({
            methodName: 'Persist',
            arguments: {
                CompositeModelId: this.compositeModelId,
                CompositeModelSource: this._compositeModelSource,
                StateObjectString: this.getJson()
            }
        });
        if (response.executed && response.succeeded) {
            return response.returnObject.key;
        }
    }

});