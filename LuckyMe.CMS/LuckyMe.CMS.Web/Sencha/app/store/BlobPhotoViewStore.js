Ext.define("Blob.store.BlobPhotoViewStore",
{
    extend: "Ext.data.Store",
    model: "Blob.model.BlobPhotoModel",
    remoteSort: true,
    sorters: [
        {
            property: "FileName",
            direction: "ASC"
        }
    ],
    pageSize: 1,
    autoLoad: true,
    autoSync: true,
    storeId: "BlobPhotoViewStoreId",

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
            read: "/Blob/GetAllBlobPhotosAsync"
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