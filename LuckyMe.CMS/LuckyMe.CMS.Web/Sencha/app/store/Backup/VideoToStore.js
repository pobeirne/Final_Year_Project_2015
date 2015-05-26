Ext.define("Backup.store.Backup.VideoToStore",
{
    extend: "Ext.data.Store",
    model: "Backup.model.Backup.VideoModel",
    remoteSort: false,
    autoLoad: false,
    autoSync: false,
    storeId: "VideoToStoreId",

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
        writer: {
            type: "json",
            allowSingle: false
        },
        api:
        {
            //read: "/Facebook/GetAllFacebookPhotosAsync"
            create: "/Blob/AddVideosToBlobAsync"
        },
        actionMethods:
        {
            //read: "GET"
            create: "POST"
        }
    }
});