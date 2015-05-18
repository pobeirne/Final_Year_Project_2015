Ext.define("Facebook.view.Facebook.PhotoGrid",
{
    extend: "Ext.grid.Panel",
    alias: "widget.PhotoGrid",
    id: "FacebookPhotoGridId",

    config:
    {
        width: "100%",
        minheight: 600,
        selType: "checkboxmodel"
    },
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
            title: "Facebook Photo Grid",
            store: "Facebook.store.FacebookPhotoStore",

            selType: this.config.selType,
            height: this.config.height,
            width: this.config.width,

            selModel:
            {
                mode: "MULTI"
            },

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
                    text: "Picture",
                    flex: 1,
                    dataIndex: "SmallPicture",
                    renderer: function (value) {
                        //return "<a href=\"" + value + "\" target=\"_blank\"> Click to view </a>";
                        return "<img height=100 width=100% src=\"" + value + "\" />";
                    }
                },
                {
                    text: "Large Picture",
                    flex: 1,
                    dataIndex: "LargePicture",
                    renderer: function (value) {
                        return "<a href=\"" + value + "\" target=\"_blank\"> Click to view </a>";
                        //return "<img src=\"" + value + "\" />";
                    }
                },
                {
                    text: "Create Date",
                    flex: 1,
                    dataIndex: "CreateDateTime",
                    renderer: Ext.util.Format.dateRenderer("d-m-Y H:i:s")
                }
            ],

            dockedItems: [
                {
                    xtype: "toolbar",
                    dock: "bottom",
                    ui: "footer",
                    layout:
                    {
                        pack: "center"
                    },
                    defaults:
                    {
                        minWidth: 150
                    },
                    items: [
                        { xtype: "tbfill" },
                        {
                            text: "Clear Filters",
                            itemId: "btnClearFilters",
                            handler: function () {
                                var grid = Ext.getCmp("FacebookPhotoGridId");
                                grid.filters.clearFilters(true);
                                grid.getStore().clearFilter();
                                grid.getStore().reload();
                            }
                        }
                    ]
                }
            ],

            bbar: Ext.create("Ext.PagingToolbar", {
                store: "Facebook.store.FacebookPhotoStore",
                displayInfo: true,
                displayMsg: "{0} - {1} of {2}",
                emptyMsg: "No topics to display"
            })
        });
        this.callParent(arguments);
    }
});
