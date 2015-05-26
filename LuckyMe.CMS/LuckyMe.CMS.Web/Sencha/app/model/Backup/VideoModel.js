Ext.define("Backup.model.Backup.VideoModel",
{
    extend: "Ext.data.Model",
    
    fields: [
        { name: "Id", type: "string" },
        { name: "Name", type: "string" },
        { name: "Description", type: "string" },
        { name: "Picture", type: "string" },
        { name: "EmbedHtml", type: "string" },
        { name: "Source", type: "string" },
        { name: "CreateDateTime", type: "date" }
    ]
});

