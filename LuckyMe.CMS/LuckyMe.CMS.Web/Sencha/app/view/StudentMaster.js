Ext.define('School.view.StudentMaster',
{
    extend: 'Ext.form.Panel',
    alias: 'widget.StudentMaster',
    id: 'StudentMasterId',

    modal: true,

    title: 'User Information',
    bodyPadding: '20',
    buttonAlign: 'center',
    layout: 'fit',

    initComponent: function() {
        this.items = [
            {
                xtype: 'form',
                layout: 'column',
                itemId: 'formPanel',
                border: 0,
                defaults: {
                    columnWidth: 1,
                    allowBlank: false,
                    style: {
                        marginBottom: '8px'
                    }
                },
                items: [
                    {
                        beforeLabelTextTpl: '<span style="color:red;font-weight:bold" data-qtip="Required">*</span>',
                        xtype: 'textfield',
                        fieldLabel: 'Name',
                        itemId: 'name',
                        name: 'name'
                    },
                    {
                        beforeLabelTextTpl: '<span style="color:red;font-weight:bold" data-qtip="Required">*</span>',
                        xtype: 'textfield',
                        fieldLabel: 'Email Address',
                        itemId: 'email',
                        name: 'email'
                    },
                    {
                        beforeLabelTextTpl: '<span style="color:red;font-weight:bold" data-qtip="Required">*</span>',
                        xtype: 'textfield',
                        fieldLabel: 'Phone Number',
                        itemId: 'phone',
                        name: 'phone'
                    }
                ]
            }
        ];

        this.buttons = [
            {
                text: 'Save',
                itemId: 'saveButton',
                handler: function() {
                }
            }, {
                text: 'Cancel',
                itemId: 'cancelButton',
                handler: function() {
                }
            }
        ];
        this.callParent();
    }
});


