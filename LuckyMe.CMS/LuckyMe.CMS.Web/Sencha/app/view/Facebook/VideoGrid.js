Ext.define("Facebook.view.Facebook.VideoGrid",
{
    extend: "Ext.grid.Panel",
    alias: "widget.VideoGrid",
    id: "FacebookVideoGridId",

    viewConfig:
    {
        stripeRows: true,
        trackOver: true
    },

    constructor: function (config) {
        this.initConfig(config);
        return this.callParent(arguments);
    },

    
    initComponent: function () {
        Ext.apply(this,
        {
            
            store: "Facebook.store.FacebookVideoStore",

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
                    renderer: function (value, metadata) {
                        metadata.tdAttr = "data-qtip=\"" + value + "\"";
                        return value;
                    }
                },
                {
                    text: "Name",
                    flex: 1,
                    dataIndex: "Name",
                    filter: { type: "string" },
                    renderer: function (value, metadata) {
                        metadata.tdAttr = "data-qtip=\"" + value + "\"";
                        return value;
                    }
                },
                {
                    text: "Description",
                    flex: 1,
                    dataIndex: "Description",
                    renderer: function (value, metadata) {
                        metadata.tdAttr = "data-qtip=\"" + value + "\"";
                        return value;
                    }
                },
                {
                    text: "Picture",
                    flex: 1,
                    dataIndex: "Picture",
                    renderer: function (value) {
                        return "<img height=87 width=100% src=\"" + value + "\" />";
                    }
                },
               
                {
                    text: "Source",
                    flex: 1,
                    dataIndex: "Source",
                    renderer: function (value) {
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
