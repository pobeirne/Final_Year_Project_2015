Ext.onReady(function() {
    Ext.Loader.setConfig({ enabled: true });
    Ext.application({
        requires: ['Ext.container.Viewport'],
        name: 'School',
        appFolder: 'Sencha/app',
        views: ['School.view.StudentMaster'],
        controllers: ['StudentMaster'],

        launch: function() {
            Ext.create('Ext.container.Container', {
                title: 'Test',
                renderTo: 'login',
                items: [
                    {
                        xtype: 'StudentMaster'
                    }
                ]
            });
        }
    });
});