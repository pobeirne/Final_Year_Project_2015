Ext.define("Backup.store.Backup.PhotoToStore",
{
    extend: "Ext.data.Store",
    model: "Backup.model.Backup.PhotoModel",
    remoteSort: false,
    autoLoad: false,
    autoSync: false,
    storeId: "PhotoToStoreId",
    

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
            type: "json"
            //totalProperty: "totalCount"
        },
        api:
        {
            //read: "/Facebook/GetAllFacebookPhotosAsync"
            create: "/Blob/AddPhotosToBlobAsync"
        },
        actionMethods:
        {
            //read: "GET"
            create: "POST"
        }
    }
});