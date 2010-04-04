/*!
 * LobsterDb
 * Copyright(c) 2010, LobsterDb
 * Licenced under the AGPL Version 3 license
 * http://www.lobsterdb.com/license
 */
(function() {

    var Loader = {

        loadUrl: function(url) {
            try {
                var xhr = new XMLHttpRequest();
                xhr.open("GET", url, false);
                xhr.send(null);
                if (xhr.status != 200) {
                    debugger;
                } else {
                    return (xhr.responseText);
                }
            } catch (e) {
                debugger;
            }
        },

        loadCode: function(url) {
            var code = this.loadUrl(url);
            code += '//@ sourceURL=res:/' + url;
            window.eval(code);
        },

        loadAllCode: function(rootPath, root) {
            var fileList = [];
            this.parseManifest(root, rootPath, fileList);
            //this.loadPackage(
        },

        loadManifest: function(url) {
            var str = Loader.loadUrl(url);
            try {
                //var obj = eval('({' + str + '})');
                eval(str);
                return manifest;
                //console.log('success');
            } catch (e) {
                alert(url);
                //console.log('error loading manifest:' + url);
                //debugger;
                throw new Error(e);
            }
        },

        parseManifest: function(packageObject, packagePath, fileList) {

            var manifest = this.loadManifest(packagePath + '/_Manifest.js');

            packageObject[manifest.name] = {};

            for (var x = 0; x < manifest.files.length; x++) {
                fileList.push(packagePath + '/' + manifest.files[x]);
                this.loadCode(packagePath + '/' + manifest.files[x]);
            }

            if (manifest.components) {
                for (var x = 0; x < manifest.components.length; x++) {
                    var childPackage = manifest.components[x];
                    this.parseManifest(packageObject[manifest.name],
                        packagePath + '/' + manifest.components[x], fileList
                    );
                }
            }
        }

    }
    
    Loader.loadAllCode('/Lobster', window);
    Lobster.version = '1.0.0'

    if (window.console === undefined) {
        window.console = {};
        window.console.log = function() { };
    }

})();