Ext.define("Blob.store.BlobVideoStore",
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
    pageSize: 10,
    autoLoad: true,
    autoSync: true,
    storeId: "BlobVideoStoreId"
});