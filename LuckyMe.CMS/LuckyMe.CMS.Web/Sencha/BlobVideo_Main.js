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
                id: "appContainer",
                renderTo: Ext.getElementById("app-container"),
                autoCreateViewPort: false,
                activeTab: 0,

                layout: {
                    type: "hbox",
                    align: "stretch"
                },

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

                tabBar: {
                    layout: {
                        type: "hbox",
                        align: "stretch"
                    },
                    defaults: { flex: 1 }
                },

                items:
                [
                    {
                        title: "Video Viewer",
                        height: 650,
                        items: [
                            {
                                xtype: "VideoView",
                                width: "100%",
                                layout:
                                {
                                    type: "fit",
                                    align: "left"
                                }
                            }
                        ],
                        bbar: Ext.create("Ext.PagingToolbar", {
                            store: "Blob.store.BlobVideoViewStore",
                            displayInfo: true,
                            displayMsg: "{0} - {1} of {2}",
                            emptyMsg: "No topics to display"
                        })
                    },
                    {
                        title: "Video Grid",
                        height: 650,
                        items: [
                            {
                                xtype: "VideoGrid",
                                width: "100%",
                                layout:
                                {
                                    type: "fit",
                                    align: "left"
                                }
                            }
                        ],
                        bbar: Ext.create("Ext.PagingToolbar", {
                            store: "Blob.store.BlobVideoStore",
                            displayInfo: true,
                            displayMsg: "{0} - {1} of {2}",
                            emptyMsg: "No topics to display",
                            items: [
                                {
                                    text: "Clear Filters",
                                    itemId: "btnClearFilters",
                                    handler: function() {
                                        var grid = Ext.getCmp("VideoGridId");
                                        grid.filters.clearFilters(true);
                                        grid.getStore().clearFilter();
                                        grid.getStore().reload();
                                    }
                                }
                            ]
                        })
                    }
                ]
            });
            Ext.getBody().unmask();
        }
    });

});