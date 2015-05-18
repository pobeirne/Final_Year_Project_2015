Ext.define("Blob.view.Blob.PhotoView",
{
    extend: "Ext.view.View",

    alias: "widget.PhotoView",

    requires: ["Ext.window.MessageBox"],

    constructor: function(config) {
        this.initConfig(config);
        return this.callParent(arguments);
    },

    config: {
        tpl: new Ext.XTemplate(
            "<div class=\"jumbotron\" style=\"padding:20px;margin:20px;\">",
            "<tpl for=\".\">",
            "<h4>File name:{FileName}</h4>",
            "<div style=\"height:400px;overflow:hidden;\"><img src=\"{FileUrl}\" alt=\"\" style=\"height:100%;width:100%;\"></div>",
            "<h4>Create Date: {LastModified}</h4>",
            "</div>",
            "</tpl>"
        )
    },
    initComponent: function() {

        Ext.apply(this,
        {
            id: "PhotoViewId",
            title: "Photo Viewer",
            autoScroll: true,
            bodyPadding: "5 5 5 5",
            store: "Blob.store.BlobPhotoViewStore",
            itemSelector: "div.studentinfo",
            emptyText: "No images available",

            listeners:
            {
                itemclick:
                {
                    fn: function(me, record) {
                        Ext.Msg.alert("Item clicked", "You clicked on : " + record.get("FileName"));
                    }
                }
            }
        });
        this.callParent(arguments);
    }
});