Ext.define("Backup.view.Backup.VideoToGrid",
{
    extend: "Ext.grid.Panel",
    alias: "widget.VideoToGrid",
    id: "VideoToGridId",

    //config:
    //{
    //    width: "100%",
    //    minheight: 600,
    //    selType: "checkboxmodel"
    //},
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
            title: "Video To Grid",
            store: "Backup.store.Backup.VideoToStore",

            //selType: this.config.selType,
            //height: this.config.height,
            //width: this.config.width,

            //selModel:
            //{
            //    mode: "MULTI"
            //},

            //features: [
            //    {
            //        ftype: "filters",
            //        encode: true,
            //        local: false
            //    }
            //],

            viewConfig: {
                plugins: {
                    copy: false,
                    ptype: "gridviewdragdrop",
                    //dragGroup: "secondGridDDGroup",
                    dropGroup: "firstGridDDGroup"
                },
                listeners: {
                    drop: function (node, data, dropRec, dropPosition) {
                        var dropOn = dropRec ? " " + dropPosition + " " + dropRec.get("Name") : " on empty view";
                        Ext.example.msg("Drag from left to right", "Dropped " + data.records[0].get("Name") + dropOn);
                    },
                    'beforedrop': function (node, data, overModel, dropPosition, dropHandlers) {
                        var record = data.records[0];
                        if (this.getStore().findBy(function (r) {
                            return r.get("Id") === record.get("Id");
                        }) === -1) {
                            console.log("Process drop");
                            dropHandlers.processDrop();
                        } else {
                            console.log("Cancel drop");
                            dropHandlers.cancelDrop();
                        }
                    }
                }
            },

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
                    //filter: { type: "string" },
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
                        //return "<a href=\"" + value + "\" target=\"_blank\"> Click to view </a>";
                        return "<img height=100 width=100% src=\"" + value + "\" />";
                    }
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
                         {
                             text: "Clear All",
                             itemId: "btnClear",
                             handler: function () {
                                 var grid = Ext.getCmp("VideoToGridId");
                                 grid.getStore().removeAll();
                             }
                         },
                         {
                             text: "Save",
                             itemId: "btnSave",
                             action:"SaveVideosToBlob"
                         }
                     ]
                 }
            ]

            //bbar: Ext.create("Ext.PagingToolbar", {
            //    store: "Backup.store.Backup.VideoToStore",
            //    displayInfo: true,
            //    displayMsg: "{0} - {1} of {2}",
            //    emptyMsg: "No topics to display"
            //})
        });
        this.callParent(arguments);
    }
});
