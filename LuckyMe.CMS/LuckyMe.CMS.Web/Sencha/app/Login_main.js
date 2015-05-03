Ext.Loader.setConfig({ enabled: true });
Ext.application({
    name: 'AM',
    appFolder: 'Sencha/app',
    controllers: [
        ''
    ],
    launch: function() {
        Ext.create('Ext.Panel', {
            title: 'Test',
            layout: 'fit',
            renderTo: 'login',
            margins: '5 5 5 5',
            items: {
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
                ],
                region: 'center'
            }
        });
    }
});