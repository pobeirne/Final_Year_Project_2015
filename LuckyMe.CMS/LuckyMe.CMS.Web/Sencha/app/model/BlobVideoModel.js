Ext.define("Blob.model.BlobVideoModel",
{
    extend: "Ext.data.Model",
    idProperty: "Id",
    fields: [
        { name: "FileName", type: "string" },
        { name: "AlbumName", type: "string" },
        { name: "Directory", type: "string" },
        { name: "FileUrl", type: "string" },
        { name: "ContentType", type: "string" },
        { name: "Size", type: "long" },
        { name: "LastModified", type: "date", dateFormat: "MS" }
    ],
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
            read: "/Blob/GetAllBlobVideosAsync",
            update: "/Blob/UpdatelBlobVideoAsync",
            destroy: "/Blob/DeleteBlobVideoAsync"
        },
        actionMethods:
        {
            read: "GET",
            update: "POST",
            destroy: "POST"
        }
    }
});

