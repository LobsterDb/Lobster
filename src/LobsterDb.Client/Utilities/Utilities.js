
Lobster.Utilities.Utilities = {

    extend: function(subclass, superclass, methods) {
        //subClass, superClass are js functions
        //branches superClass to protect it
        var superBranch = function(){};
        superBranch.prototype = superclass.prototype;
        var proto = new superBranch();
        proto.constructor = subclass;
        //subClass.prototype.c = subClass;
        proto.superclass = superclass;
        subclass.superclass = superclass;
        //add-on subProto methods
        for (var name in methods) {
            //hasOwnProperty to filter out json.org Object
            //prototype modifications
            if (methods.hasOwnProperty(name)) {
                proto[name] = methods[name];
            }
        }
        subclass.prototype = proto;
    }

}