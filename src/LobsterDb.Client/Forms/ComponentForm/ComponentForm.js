/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
Lobster.Forms.ComponentForm.ComponentForm = function(args){
    Lobster.Forms.ComponentForm.ComponentForm.superclass.call(this, args);
}

Lobster.Forms.ComponentForm.ComponentForm.getNew = function(args){
    args.composite = args.component;
    var form = new Lobster.Forms.ComponentForm.ComponentForm(args);
    return form;
}

Lobster.Utilities.Utilities.extend(Lobster.Forms.ComponentForm.ComponentForm,
    Lobster.Forms.Form.Form, {
    
    
});    