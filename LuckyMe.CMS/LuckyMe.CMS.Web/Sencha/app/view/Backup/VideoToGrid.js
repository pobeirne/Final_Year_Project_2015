Ext.define("Backup.view.Backup.VideoToGrid",
{
    extend: "Ext.grid.Panel",
    alias: "widget.VideoToGrid",
    id: "VideoToGridId",

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

         
            viewConfig: {
                plugins: {
                    copy: false,
                    ptype: "gridviewdragdrop",
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
                        return "<img height=73 width=100% src=\"" + value + "\" />";
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
        });
        this.callParent(arguments);
    }
});
