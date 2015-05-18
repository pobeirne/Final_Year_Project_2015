Ext.define("Facebook.model.FacebookVideoModel",
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
    ]
});

