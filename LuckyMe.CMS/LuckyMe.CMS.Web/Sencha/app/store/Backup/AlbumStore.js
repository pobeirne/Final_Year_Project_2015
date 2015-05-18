Ext.define("Backup.store.Backup.AlbumStore",
{
    extend: "Ext.data.Store",
    model: "Backup.model.Backup.AlbumModel",
    remoteSort: false,
    
    autoLoad: true,
    autoSync: true,
    storeId: "AlbumStoreId",

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
            read: "/Blob/GetAllPhotoAlbumsAsync"
        },
        actionMethods:
        {
            read: "GET"
        }
    }
});