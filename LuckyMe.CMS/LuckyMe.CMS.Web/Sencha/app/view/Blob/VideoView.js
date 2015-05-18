Ext.define("Blob.view.Blob.VideoView",
{
    extend: "Ext.view.View",

    alias: "widget.VideoView",

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
            "<div class=\"embed-responsive embed-responsive-16by9\" style=\"height:400px;padding:20px;margin:20px;\">",
            "<iframe class=\"embed-responsive-item\" src=\"{FileUrl}\"></iframe>",
            "</div>",
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
            store: "Blob.store.BlobVideoViewStore",
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