Ext.define("Backup.store.Backup.PhotoFromStore",
{
    extend: "Ext.data.Store",
    model: "Backup.model.Backup.PhotoModel",
    remoteSort: true,
    sorters: [
        {
            property: "Name",
            direction: "ASC"
        }
    ],
    pageSize: 4,
    autoLoad: true,
    autoSync: true,
    storeId: "PhotoFromStoreId",
    proxy:
    {
        type: "ajax",
        timeout: 100000,
        headers:
        {
            'Content-Type': "application/json; charset=UTF-8"
        },
        reader:
        {
            root: "data",
            type: "json",
            totalProperty: "totalCount"
        },
        api:
        {
            read: "/Facebook/GetAllFacebookPhotosAsync"
        },
        actionMethods:
        {
            read: "GET"
        }
    }
});