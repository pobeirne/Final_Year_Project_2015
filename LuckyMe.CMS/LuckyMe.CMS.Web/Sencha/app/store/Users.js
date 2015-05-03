Ext.define('AM.store.Users', {
    extend: 'Ext.data.Store',
    model: 'AM.model.User',
    autoLoad: true,
    autoSync: true,
    proxy: {
        type: 'ajax',
        limitParam: 'size',
        startParam: undefined,
        api: {
            create: '/user/add',
            read: '/user/list',
            update: '/user/update',
            destroy: '/user/delete'
        },
        reader: {
            type: 'json',
            root: 'data',
            successProperty: 'success'
        },
        writer: {
            type: 'json',
            writeAllFields: false
        }
    }
});