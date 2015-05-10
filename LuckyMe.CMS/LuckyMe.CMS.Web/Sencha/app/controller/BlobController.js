Ext.define("Storage.controller.BlobController",
{
    extend: "Ext.app.Controller",
    views: ["Storage.view.Blob.Form"],

    init: function() {
        
        this.control({
                'BlobForm button[action=load]':
                {
                    click: this.onButtonLoadClick
                }
            }
        );
    },
    
    onButtonLoadClick: function() {
        console.log("The button was clicked");
    }
});