Ext.define("Blob.store.BlobPhotoStore",
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
    pageSize: 25,
    autoLoad: true,
    autoSync: true,
    storeId: "BlobPhotoStoreId"
});