Ext.define("Storage.view.Blob.Form",
{
    extend: "Ext.form.Panel",
    alias: "widget.BlobForm",

    initComponent: function() {

        this.id = "blobFormId";
        this.width = "100%";
        this.items = [
            {
                style: "margin: 20px",
                bodyPadding: 5,
                padding: "5 0 0 5",
                xtype: "container",
                items: [
                    {
                        xtype: "container",
                        layout: "hbox",
                        items: [
                            {
                                id: "albumSelect",
                                cls: "field-margin",
                                xtype: "combobox",
                                flex: 1,
                                fieldLabel: "Content Type",
                                padding: 10,
                                queryMode: "local",
                                store: ["Yes", "No", "Maybe"],
                                matchFieldWidth: true

                            }, {
                                cls: "field-margin",
                                xtype: "combobox",
                                flex: 1,
                                fieldLabel: "Content Type",
                                padding: 10,
                                queryMode: "local",
                                store: ["Yes", "No", "Maybe"],
                                matchFieldWidth: true
                            },
                            {
                                xtype: "button",
                                itemId: "loadGridId",
                                text: "Load",
                                action: "load",
                                cls: "field-margin",
                                flex: 0.5,
                                padding: 5,
                                margin: 10
                            }
                        ]
                    }
                ]
            }
        ];

        this.callParent();
    }
});