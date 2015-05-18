Ext.define("Facebook.model.FacebookPhotoModel",
{
    extend: "Ext.data.Model",

    idProperty: "Id",

    fields: [
        { name: "Id", type: "string" },
        { name: "Name", type: "string" },
        { name: "SmallPicture", type: "string" },
        { name: "LargePicture", type: "string" },
        { name: "CreateDateTime", type: "date", dateFormat: "MS" }
    ],
    validations: [
        {
            type: "presence",
            field: "Id"
        }
    ]
});

