Ext.define("Facebook.view.Facebook.PhotoView",
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
            "<tpl for=\".\">",
            "<div class=\"jumbotron\" style=\"padding:20px;margin:20px;\">",
            "<h4>File name:{Name}</h4>",
            "<div style=\"height:450px;\"><img src=\"{LargePicture}\" alt=\"\" style=\"height:100%;width:100%;\"></div>",
            "<h4>Create Date: {CreateDateTime}</h4>",
            "</div>",
            "</tpl>"
        )
    },
    initComponent: function() {

        Ext.apply(this,
        {
            id: "FacebookPhotoViewId",
            title: "Photo Viewer",
            autoScroll: true,
            bodyPadding: "5 5 5 5",
            store: "Facebook.store.FacebookPhotoViewStore",
            itemSelector: "div.jumbotron",
            emptyText: "No images available",
            listeners:
            {
                itemclick:
                {
                    fn: function(me, record) {
                        Ext.Msg.alert("Item clicked", "You clicked on : " + record.get("Name"));
                    }
                }
            }
        });
        this.callParent(arguments);
    }
});