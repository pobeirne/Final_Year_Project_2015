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
        "Ext.dd.*",
        "Ext.ux.grid.FiltersFeature",
        "Ext.example"
    ]);

    Ext.application({
        name: "Backup",

        appFolder: "/Sencha/app",

        controllers: [
            "Backup.controller.BackupController"
        ],


        launch: function() {
            Ext.create("Ext.tab.Panel", {
                renderTo: Ext.getElementById("app-container"),
                id: "appContainer",
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
                        title: "Photo Tab",
                        bodyStyle: "padding: 10px;",
                        layout: {
                            type: "hbox",
                            align: "left"
                        },
                        items:
                        [
                            {
                                xtype: "PhotoFromGrid",
                                minHeight: 600,
                                width: "49%"
                            },
                            { xtype: "tbfill", width: 2 },
                            {
                                xtype: "PhotoToGrid",
                                minHeight: 600,
                                maxHeight: 600,
                                width: "49%"
                            }
                        ],
                        dockedItems: {
                            xtype: "toolbar",
                            dock: "top",
                            items: [
                                "->", // Fill
                                {
                                    xtype: "combobox",
                                    id: "photoComboboxId",
                                    labelWidth: 200,
                                    fieldLabel: "Select/Enter Album name",
                                    emptyText: "Default",
                                    valueField:"AlbumName",
                                    padding: "10",
                                    width: "50%",
                                    store: "Backup.store.Backup.AlbumStore",
                                    matchFieldWidth: true,
                                    queryMode: "local"
                                    
                                }
                            ]
                        }
                    },
                    {
                        title: "Video Tab",
                        bodyStyle: "padding: 10px;",
                        layout: {
                            type: "hbox",
                            align: "left"
                        },
                        items:
                        [
                            {
                                xtype: "VideoFromGrid",
                                minHeight: 600,
                                width: "49%"
                            },
                            { xtype: "tbfill", width: 2 },
                            {
                                xtype: "VideoToGrid",
                                minHeight: 600,
                                maxHeight: 600,
                                width: "49%"
                            }
                        ],
                        dockedItems: {
                            xtype: "toolbar",
                            dock: "top",
                            items: [
                                "->", // Fill
                                {
                                    xtype: "combobox",
                                    id: "videoComboboxId",
                                    labelWidth: 200,
                                    fieldLabel: "Select/Enter Album name",
                                    emptyText: "Default",
                                    padding: "10",
                                    width: "50%",
                                    store: ["Yes", "No", "Maybe"],
                                    matchFieldWidth: true
                                }
                            ]
                        }
                    }
                ]
            });

            Ext.getBody().unmask();
        }
    });
});



