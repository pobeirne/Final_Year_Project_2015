Ext.define("Facebook.view.Facebook.VideoView",
{
    extend: "Ext.view.View",

    alias: "widget.VideoView",

    requires: ["Ext.window.MessageBox"],

    constructor: function (config) {
        this.initConfig(config);
        return this.callParent(arguments);
    },

    config: {
        tpl: new Ext.XTemplate(

            "<div class=\"jumbotron\" style=\"padding:20px;margin:20px;\">",
            "<tpl for=\".\">",
            "<h4>File name:{Name}</h4>",
            "<div class=\"embed-responsive embed-responsive-16by9\" >",
            "<video class=\"embed-responsive-item\" controls>",
                "<source src=\"{Source}\"type=video/mp4>" +
            "</video>",
            "</div>",
            "<h4>Create Date: {CreateDateTime}</h4>",
            "</div>",
            "</tpl>"
            )
    },
    initComponent: function () {

        Ext.apply(this,
        {
            id: "FacebookVideoViewId",
            title: "Video Viewer",
            autoScroll: true,
            bodyPadding: "5 5 5 5",
            store: "Facebook.store.FacebookVideoViewStore",
            itemSelector: "div.studentinfo",
            emptyText: "No images available",
            listeners:
            {
                itemclick:
                {
                    fn: function (me, record) {
                        Ext.Msg.alert("Item clicked", "You clicked on : " + record.get("Name"));
                    }
                }
            }
        });
        this.callParent(arguments);
    }
});