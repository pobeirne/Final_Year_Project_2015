Ext.onReady(function() {

    Ext.Loader.setConfig(
    {
        enabled: true
    });

    Ext.getBody().mask("Loading");

    Ext.Loader.setPath("Ext.ux", "Sencha/library/ux");

    Ext.application(
    {
        name: "School",
        controllers: ["StudentMaster"],
        appFolder: "Sencha/app",


        launch: function() {

            Ext.create("Ext.container.Container",
            {
                id: "FaceBookPhotosGridContainerId",
                padding: "0 0 0 0",
                width: "100%",
                renderTo: "panel",
                border: true,
                autoShow: true,
                title: "Grid Test",
                layout:
                {
                    type: "fit",
                    align: "left"
                },

                items:
                [
                    {
                        xtype: "panel",
                        items: [{
                            xtype: "combobox",
                            fieldLabel: "Short Options",
                            padding: "10",
                            //width: "100%",
                            queryMode: "local",
                            store: ["Yes", "No", "Maybe"],
                            matchFieldWidth: true
                            //listConfig: {
                            //    listeners: {
                            //        beforeshow: function (picker) {
                            //            picker.minWidth = 200;
                            //        }
                            //    }
                            //}
                        }]
                    },
                    {
                      
                    },
                    {
                        xtype: "StudentGrid",
                        padding: "10 0 0 0",
                        width: "100%"
                    }
                ]
            });
            Ext.getBody().unmask();
        }
    });

});