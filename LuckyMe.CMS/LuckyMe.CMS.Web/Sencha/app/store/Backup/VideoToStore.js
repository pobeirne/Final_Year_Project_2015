Ext.define("Backup.store.Backup.VideoToStore",
{
    extend: "Ext.data.Store",
    model: "Backup.model.Backup.VideoModel",
    remoteSort: false,
    autoLoad: false,
    autoSync: false,
    storeId: "VideoToStoreId"
});