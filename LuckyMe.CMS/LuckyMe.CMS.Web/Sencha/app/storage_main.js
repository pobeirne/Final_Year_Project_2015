Ext.application({
    name: "Storage",
    appFolder: "Sencha/app",
    controllers: [
        "BlobController"
    ],
    launch: function() {
        Ext.create("Ext.container.Container", {
            id: "BlobContainerId",
            padding: "0 0 0 0",
            width: "100%",
            border: true,
            autoShow: true,
            layout:
            {
                type: "fit",
                align: "left"
            },
            items:
            [
                {
                    title: "Blob Storage",
                    xtype: "panel",
                    width: "100%",
                    items: [
                        {
                            xtype: "BlobForm"
                        },
                        {
                            xtype: "panel",
                            minHeight: 300
                        }
                    ],
                    layout:
                    {
                        type: "fit",
                        align: "left"
                    }
                }
            ],
            renderTo: "blobcontainer"
        });
    }
});
