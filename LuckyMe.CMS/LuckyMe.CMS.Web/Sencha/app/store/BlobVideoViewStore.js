Ext.define("Blob.store.BlobVideoViewStore",
{
    extend: "Ext.data.Store",
    model: "Blob.model.BlobVideoModel",
    remoteSort: true,
    sorters: [
        {
            property: "FileName",
            direction: "ASC"
        }
    ],
    pageSize: 1,
    autoLoad: true,
    autoSync: false,
    storeId: "BlobVideoViewStoreId",
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
            read: "/Blob/GetAllBlobVideosAsync"
            //,
            //create: "/ExampleService.svc/createstudent/",
            //update: "/ExampleService.svc/updatestudent/",
            //destroy: "/ExampleService.svc/deletestudent/"
        },
        actionMethods:
        {
            read: "GET"
            //,
            //create: "POST",
            //update: "POST",
            //destroy: "POST"
        }
    }
});