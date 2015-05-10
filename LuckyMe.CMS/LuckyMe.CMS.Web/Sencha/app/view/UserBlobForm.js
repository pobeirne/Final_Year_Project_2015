Ext.define("Storage.view.UserBlobForm",
{
    extend: "Ext.form.Panel",
    alias: "widget.UserBlobForm",

    initComponent: function() {

        this.id = "frmCloneUser_id";
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
                                id:"albumSelect",
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
                                 cls: "field-margin",
                                 flex:0.5,
                                 padding: 5,
                                 margin: 10,
                                 xtype: "button",
                                 text: "Load",
                                 itemId: "load"
                             }
                           
                        ]
                    }
                ]
            }
        ];

        this.callParent();
    }
});