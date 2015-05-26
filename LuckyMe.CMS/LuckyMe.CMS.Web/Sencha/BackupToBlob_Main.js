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
                minHeight: 600,
                activeTab: 0,

               

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
                        title: "Photo Backup",
                        //bodyStyle: "padding: 10px;",
                        height: 650,
                        layout: {
                            type: "hbox",
                            align: "left"
                        },
                        items:
                        [
                            {
                                xtype: "PhotoFromGrid",
                                height: 611,
                                width: "49%"
                            },
                            { xtype: "tbfill", width: 2 },
                            {
                                xtype: "PhotoToGrid",
                                height: 611,
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
                                   
                                    width: "50%",
                                    store: "Backup.store.Backup.AlbumStore",
                                    matchFieldWidth: true,
                                    queryMode: "local"
                                    
                                }
                            ]
                        }
                    },
                    {
                        title: "Video Backup",
                        height: 650,
                        layout: {
                            type: "hbox",
                            align: "left"
                        },
                        items:
                        [
                            {
                                xtype: "VideoFromGrid",
                                height: 611,
                                width: "49%"
                            },
                            { xtype: "tbfill", width: 1 },
                            {
                                xtype: "VideoToGrid",
                                height: 611,
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
                                     valueField: "AlbumName",

                                     width: "50%",
                                     store: "Backup.store.Backup.AlbumStore",
                                     matchFieldWidth: true,
                                     queryMode: "local"

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



