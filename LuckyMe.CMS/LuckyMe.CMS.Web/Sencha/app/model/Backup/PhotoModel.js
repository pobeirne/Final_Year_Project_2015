Ext.define("Backup.model.Backup.PhotoModel",
{
    extend: "Ext.data.Model",

    
    fields: [
        { name: "Id", type: "string" },
        { name: "Name", type: "string" },
        { name: "SmallPicture", type: "string" },
        { name: "LargePicture", type: "string" },
        { name: "CreateDateTime", type: "date", dateFormat: "MS" }
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

