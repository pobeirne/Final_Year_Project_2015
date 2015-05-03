Ext.define('AM.view.user.Edit', {
    extend: 'Ext.window.Window',
    alias: 'widget.useredit',
    title: 'Edit User',
    layout: 'fit',
    autoShow: true,
    initComponent: function () {
        this.items = [
            {
                xtype: 'form',
                bodyStyle: {
                    background: 'none',
                    padding: '10px',
                    border: '0'
                },
                items: [
                    {
                        xtype: 'textfield',
                        name: 'name',
                        allowBlank: false,
                        fieldLabel: 'Name'
                    },
                    {
                        xtype: 'textfield',
                        name: 'email',
                        allowBlank: false,
                        vtype: 'email',
                        fieldLabel: 'Email'
                    }
                ]
            }
        ];
        this.buttons = [
            {
                text: 'Save',
                action: 'save'
            },
            {
                text: 'Cancel',
                scope: this,
                handler: this.close
            }
        ];
        this.callParent(arguments);
    }
});