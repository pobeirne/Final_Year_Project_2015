Ext.define("Facebook.store.FacebookPhotoStore",
{
    extend: "Ext.data.Store",
    model: "Facebook.model.FacebookPhotoModel",
    remoteSort: true,
    sorters: [
        {
            property: "Name",
            direction: "ASC"
        }
    ],
    pageSize: 10,
    autoLoad: true,
    autoSync: true,
    storeId: "FacebookPhotoStoreId",
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