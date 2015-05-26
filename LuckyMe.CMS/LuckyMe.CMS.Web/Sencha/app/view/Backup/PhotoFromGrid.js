Ext.define("Backup.view.Backup.PhotoFromGrid",
{
    extend: "Ext.grid.Panel",
    alias: "widget.PhotoFromGrid",
    id: "PhotoFromGridId",
   
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
            title: "Photo From Grid",
            store: "Backup.store.Backup.PhotoFromStore",

         
            height: this.config.height,
            width: this.config.width,
            

            features: [
                {
                    ftype: "filters",
                    encode: true,
                    local: false
                }
            ],

            viewConfig: {
                copy: true,
                plugins: {
                    ptype: "gridviewdragdrop",
                    dragGroup: "firstGridDDGroup",
                    dropGroup: "secondGridDDGroup"
                },
                listeners: {
                    drop: function(node, data, dropRec, dropPosition) {
                        var dropOn = dropRec ? " " + dropPosition + " " + dropRec.get("Name") : " on empty view";
                        Ext.example.msg("Drag from right to left", "Dropped " + data.records[0].get("Name") + dropOn);
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
                        return "<img height=73 width=100% src=\"" + value + "\" />";
                    }
                },
                {
                    text: "Large Picture",
                    flex: 1,
                    dataIndex: "LargePicture",
                    hidden: true,
                    renderer: function (value) {
                        return "<a href=\"" + value + "\" target=\"_blank\"> Click to view </a>";
                        //return "<img src=\"" + value + "\" />";
                    }
                },
                {
                    text: "Create Date",
                    flex: 1,
                    dataIndex: "CreateDateTime",
                    hidden: true,
                    renderer: Ext.util.Format.dateRenderer("d-m-Y H:i:s")
                }
            ],
           

            bbar: Ext.create("Ext.PagingToolbar", {
                store: "Backup.store.Backup.PhotoFromStore",
                displayInfo: true,
                displayMsg: "{0} - {1} of {2}",
                emptyMsg: "No topics to display",
                items: [
                        {
                            text: "Clr",
                            itemId: "btnClearFilters",
                            handler: function () {
                                var grid = Ext.getCmp("PhotoFromGridId");
                                grid.filters.clearFilters(true);
                                grid.getStore().clearFilter();
                                grid.getStore().reload();
                            }
                        }
                ]

            })
        });
        this.callParent(arguments);
    }
});
