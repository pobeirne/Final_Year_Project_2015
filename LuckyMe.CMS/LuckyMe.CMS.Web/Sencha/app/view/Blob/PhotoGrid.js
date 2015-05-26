Ext.define("Blob.view.Blob.PhotoGrid",
{
    extend: "Ext.grid.Panel",
    alias: "widget.PhotoGrid",
    id: "PhotoGridId",

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
            title: "File Grid",
            store: "Blob.store.BlobPhotoStore",
            features: [
                {
                    ftype: "filters",
                    encode: true,
                    local: false
                   
                }
            ],

            plugins: [
                Ext.create("Ext.grid.plugin.RowEditing",
                {
                    clicksToEdit: 2
                })
            ],

            columns: [
                {
                    text: "File Name",
                    flex: 1,
                    dataIndex: "FileName",
                    filter: {
                        type: "string"
                        
                    },
                    editor:
                    {
                        allowBlank: false
                    },
                    renderer: function(value, metadata) {
                        metadata.tdAttr = "data-qtip=\"" + value + "\"";
                        return value;
                    }
                },
                {
                    text: "Album Name",
                    flex: 1,
                    dataIndex: "AlbumName",
                    renderer: function(value, metadata) {
                        metadata.tdAttr = "data-qtip=\"" + value + "\"";
                        return value;
                    }
                },
                {
                    text: "Directory",
                    flex: 1,
                    dataIndex: "Directory",
                    editor:
                    {
                        allowBlank: false
                    },
                    renderer: function(value, metadata) {
                        metadata.tdAttr = "data-qtip=\"" + value + "\"";
                        return value;
                    }
                },
                {
                    text: "FileUrl",
                    flex: 1,
                    dataIndex: "FileUrl",
                    editor:
                    {
                        allowBlank: false
                    },
                    renderer: function(value) {
                        return "<a href=\"" + value + "\" target=\"_blank\"> Click to view </a>";
                    }
                },
                {
                    text: "ContentType",
                    flex: 1,
                    dataIndex: "ContentType",
                    editor:
                    {
                        allowBlank: true
                    },
                    renderer: function(value, metadata) {
                        metadata.tdAttr = "data-qtip=\"" + value + "\"";
                        return value;
                    }
                },
                {
                    text: "Size KB",
                    flex: 1,
                    dataIndex: "Size",
                    editor:
                    {
                        allowBlank: true
                    },
                    renderer: function(value, metadata) {
                        metadata.tdAttr = "data-qtip=\"" + value + "\"";
                        return value;
                    }
                },
                {
                    text: "Last Modified",
                    flex: 1,
                    dataIndex: "LastModified",
                    editor:
                    {
                        xtype: "datefield",
                        format: "d-m-Y H:i:s",
                        allowBlank: true
                    },
                    renderer: Ext.util.Format.dateRenderer("d-m-Y H:i:s")
                },

                {
                    xtype: "actioncolumn",
                    width: 40,
                    tdCls: "delete",
                    items: [{
                        icon: "../Content/images/delete.png",  // Use a URL in the icon config
                        tooltip: "Delete",
                        handler: function (grid, rowIndex) {
                            var rec = grid.getStore().getAt(rowIndex);
                            rec.destroy();
                            //grid.getStore().remove(rec);
                            
                        }
                    }]
                }
  
            ]
           
        });
        this.callParent(arguments);
    }
});
