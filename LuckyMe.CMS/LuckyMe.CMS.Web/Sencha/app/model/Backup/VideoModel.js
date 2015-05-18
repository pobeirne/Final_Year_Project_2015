Ext.define("Backup.model.Backup.VideoModel",
{
    extend: "Ext.data.Model",

    idProperty: "Id",

    fields: [
        { name: "Id", type: "string" },
        { name: "Name", type: "string" },
        { name: "Description", type: "string" },
        { name: "Picture", type: "string" },
        { name: "EmbedHtml", type: "string" },
        { name: "Source", type: "string" },
        { name: "CreateDateTime", type: "date" }
    ],
    validations: [
        {
            type: "presence",
            field: "Id"
        }
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

