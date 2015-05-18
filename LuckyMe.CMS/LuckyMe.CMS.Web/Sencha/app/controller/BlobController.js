Ext.define("Blob.controller.BlobController",
{
    extend: "Ext.app.Controller",

    models: [
        "Blob.model.BlobPhotoModel",
        "Blob.model.BlobVideoModel"
    ],

    stores: [
        "Blob.store.BlobPhotoStore",
        "Blob.store.BlobPhotoViewStore",
        "Blob.store.BlobVideoStore",
        "Blob.store.BlobVideoViewStore"
    ],

    views: [
        "Blob.view.Blob.PhotoGrid",
        "Blob.view.Blob.PhotoView",
        "Blob.view.Blob.VideoGrid",
        "Blob.view.Blob.VideoView"
    ],


    refs: [
        {
            ref: "photoGrid",
            selector: "PhotoGrid"
        },
        {
            ref: "videoGrid",
            selector: "VideoGrid"
        }
    ],


    init: function() {

        this.control({
                'PhotoGrid button[action=delete]':
                {
                    click: this.onPhotoDeleteClick
                },
                'VideoGrid button[action=delete]':
                {
                    click: this.onVideoDeleteClick
                }
            }
        );
    },
    onPhotoDeleteClick: function() {
        //console.log("The button was clicked1");

        var me = this,
            grid = me.getPhotoGrid(),
            selectedRecord = grid.getSelectionModel().getSelection();

        var store = grid.getStore();

        if (selectedRecord.length)
            store.remove(selectedRecord);
        else
            Ext.Msg.alert("Status", "Please select at least one record to delete!");
    },
    onVideoDeleteClick: function() {
        //console.log("The button was clicked1");

        var me = this,
            grid = me.getVideoGrid(),
            selectedRecord = grid.getSelectionModel().getSelection();

        var store = grid.getStore();

        if (selectedRecord.length)
            store.remove(selectedRecord);
        else
            Ext.Msg.alert("Status", "Please select at least one record to delete!");
    }
});