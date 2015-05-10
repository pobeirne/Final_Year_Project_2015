Ext.define("School.view.StudentGrid",
{

    extend: "Ext.grid.Panel",
    alias: "widget.StudentGrid",
    //requires: ['School.view.TreeContextMenu'],

    id: "studentsGrid",
    config:
    {
        width: "100%",
        height: 400,
        selType: "checkboxmodel"
    },

    constructor: function (config) {
        this.initConfig(config);
        return this.callParent(arguments);
    },
    viewConfig:
    {
        stripeRows: true
    },
    initComponent: function () {
        Ext.apply(this,
        {
            store: "School.store.Student",

            plugins: [Ext.create("Ext.grid.plugin.RowEditing",
            {
                clicksToEdit: 2
                //if you have checkbox in first row then take clicksToEdit=2 otherwise it will go on edit mode
            })],
            selType: this.config.selType,
            height: this.config.height,
            width: this.config.width,

            selModel:
            {
                mode: "MULTI"
            },
            columns: [{
                text: "Id",
                dataIndex: "Id",
                hidden: false,
                width: 35
            },
            {
                text: "First Name",
                flex: 1,
                dataIndex: "firstName",
                editor:
                {
                    // defaults to textfield if no xtype is supplied
                    allowBlank: false
                }
            },
            {
                text: "Middle Name",
                flex: 1,
                dataIndex: "middleName",
                editor:
                {
                    // defaults to textfield if no xtype is supplied
                    allowBlank: true
                }
            },
            {
                text: "Last Name",
                flex: 1,
                dataIndex: "lastName",
                editor:
                {
                    // defaults to textfield if no xtype is supplied
                    allowBlank: true
                }
            },
            {
                text: "Birth Date",
                flex: 1,
                dataIndex: "birthDate",
                editor:
                {
                    xtype: "datefield",
                    format: "d-m-Y",
                    allowBlank: true
                },
                renderer: Ext.util.Format.dateRenderer("d-m-Y")
            },
            {
                text: "City",
                flex: 1,
                dataIndex: "city",
                editor:
                {
                    // defaults to textfield if no xtype is supplied
                    allowBlank: true
                }
            },
            {
                text: "State",
                flex: 1,
                dataIndex: "state",
                editor:
                {
                    // defaults to textfield if no xtype is supplied
                    allowBlank: true
                }
            }],
            dockedItems: [{
                xtype: "toolbar",
                dock: "bottom",
                ui: "footer",
                layout:
                {
                    pack: "center"
                },
                defaults:
                {
                    minWidth: 80
                },
                items: [{
                    text: "Create",
                    itemId: "btnCreate"
                },
                {
                    text: "Load Data",
                    itemId: "btnLoad"
                },
                {
                    text: "Delete",
                    itemId: "btnDelete"
                }]
            }]
        });

        this.callParent(arguments);
    }
});