Ext.define("Facebook.view.Facebook.PhotoGrid",
{
    extend: "Ext.grid.Panel",
    alias: "widget.PhotoGrid",
    id: "FacebookPhotoGridId",

    viewConfig:
    {
        stripeRows: true,
        trackOver: true
    },

    constructor: function(config) {
        this.initConfig(config);
        return this.callParent(arguments);
    },


    initComponent: function() {
        Ext.apply(this,
        {
            store: "Facebook.store.FacebookPhotoStore",

            features: [
                {
                    ftype: "filters",
                    encode: true,
                    local: false
                }
            ],

            columns: [
                {
                    text: "Id",
                    flex: 1,
                    dataIndex: "Id",
                    renderer: function(value, metadata) {
                        metadata.tdAttr = "data-qtip=\"" + value + "\"";
                        return value;
                    }
                },
                {
                    text: "Name",
                    flex: 1,
                    dataIndex: "Name",
                    filter: { type: "string" },
                    renderer: function(value, metadata) {
                        metadata.tdAttr = "data-qtip=\"" + value + "\"";
                        return value;
                    }
                },
                {
                    text: "Picture",
                    flex: 1,
                    dataIndex: "SmallPicture",
                    renderer: function(value) {
                        return "<img height=90 width=100% src=\"" + value + "\" />";
                    }
                },
                {
                    text: "Large Picture",
                    flex: 1,
                    dataIndex: "LargePicture",
                    renderer: function(value) {
                        return "<a href=\"" + value + "\" target=\"_blank\"> Click to view </a>";
                    }
                },
                {
                    text: "Create Date",
                    flex: 1,
                    dataIndex: "CreateDateTime",
                    renderer: Ext.util.Format.dateRenderer("d-m-Y H:i:s")
                }
            ]
        });
        this.callParent(arguments);
    }
});
