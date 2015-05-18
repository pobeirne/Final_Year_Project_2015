Ext.onReady(function() {

    Ext.Loader.setConfig(
    {
        enabled: true
    });

    Ext.getBody().mask("Loading");

    Ext.Loader.setPath("Ext.ux", "../Sencha/library/ux");
    Ext.require([
        "Ext.grid.*",
        "Ext.data.*",
        "Ext.ux.grid.FiltersFeature",
        "Ext.ux.form.SearchField"
    ]);

    Ext.application({
        name: "Blob",

        appFolder: "/Sencha/app",

        controllers: ["Blob.controller.BlobController"],


        launch: function() {
            Ext.create("Ext.tab.Panel", {
                renderTo: Ext.getElementById("app-container"),
                autoCreateViewPort: false,
                minHeight: 600,
                activeTab: 0,
                layout: {
                    type: "hbox",
                    align: "stretch"
                },
                id: "appContainer",
                listeners: {
                    beforerender: function() {
                        Ext.getCmp("appContainer").setHeight(Ext.get("app-container").getHeight);
                        Ext.getCmp("appContainer").doLayout();

                        Ext.EventManager.onWindowResize(function() {
                            Ext.getCmp("appContainer").setHeight(Ext.get("app-container").getHeight);
                            Ext.getCmp("appContainer").doLayout();
                        });
                    }
                },
                items:
                [
                    {
                        xtype: "panel",
                        width: 550,
                        title: "Blob Photo Viewer",
                        collapsible: true,
                        items: [
                            {
                                xtype: "PhotoView",
                                width: "100%",
                                layout:
                                {
                                    type: "fit",
                                    align: "left"
                                }
                            }
                        ],

                        //tbar: [
                        //    "Search : ", " ",
                        //    new Ext.ux.form.SearchField({
                        //        store: Ext.create("Storage.store.BlobPhotoView"),
                        //        width: 320
                        //    })
                        //],
                        bbar: Ext.create("Ext.PagingToolbar", {
                            store: "Blob.store.BlobPhotoViewStore",
                            displayInfo: true,
                            displayMsg: "{0} - {1} of {2}",
                            emptyMsg: "No topics to display"
                        })
                    },
                    {
                        xtype: "PhotoGrid",
                        width: "100%",
                        layout:
                        {
                            type: "fit",
                            align: "left"
                        }
                    }
                ]
            });
            Ext.getBody().unmask();
        }
    });

});



