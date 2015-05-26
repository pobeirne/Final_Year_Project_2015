Ext.define("Backup.store.Backup.VideoFromStore",
{
    extend: "Ext.data.Store",
    model: "Backup.model.Backup.VideoModel",
    remoteSort: true,
    sorters: [
        {
            property: "Name",
            direction: "ASC"
        }
    ],
    pageSize: 6,
    autoLoad: true,
    autoSync: true,
    storeId: "VideoFromStoreId",
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
            read: "/Facebook/GetAllFacebookVideosAsync"
        },
        actionMethods:
        {
            read: "GET"
        }
    }
});