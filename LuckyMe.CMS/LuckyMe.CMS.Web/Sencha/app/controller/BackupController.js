Ext.define("Backup.controller.BackupController",
{
    extend: "Ext.app.Controller",

    models: [
        "Backup.model.Backup.PhotoModel",
        "Backup.model.Backup.VideoModel",
        "Backup.model.Backup.AlbumModel"
    ],

    stores: [
        "Backup.store.Backup.PhotoFromStore",
        "Backup.store.Backup.PhotoToStore",
        "Backup.store.Backup.VideoFromStore",
        "Backup.store.Backup.VideoToStore",
        "Backup.store.Backup.AlbumStore"
    ],

    views: [
        "Backup.view.Backup.PhotoFromGrid",
        "Backup.view.Backup.PhotoToGrid",
        "Backup.view.Backup.VideoFromGrid",
        "Backup.view.Backup.VideoToGrid"
    ],


    init: function() {

        this.control({
                'PhotoToGrid button[action=SavePhotosToBlob]':
                {
                    click: this.onButtonSavePhotosClick
                },
                'VideoToGrid button[action=SaveVideosToBlob]':
                {
                    click: this.onButtonSaveVideosClick
                }
            }
        );
    },

    onButtonSavePhotosClick: function() {
        //console.log("The button was clicked1");

        var comboValue = Ext.getCmp("photoComboboxId");

        var store = Ext.getCmp("PhotoToGridId").getStore();
        
        if (comboValue.getValue() === null) {
            store.getProxy().setExtraParam("album", "pdefault");
        } else {
            store.getProxy().setExtraParam("album", comboValue.getValue());
        }
        store.save({
            success: function () {
                console.log("success!!");
                Ext.example.msg("Successfully saved !", "Album: " + comboValue.getValue() + "Record Count: " + store.getCount());
            },
            failure: function () {
                console.log("failed...");
            },
            callback: function () {
                console.log("calling callback");
            },
            scope: this
        });

        
    },

    onButtonSaveVideosClick: function() {
        //console.log("The button was clicked1");

        var comboValue = Ext.getCmp("videoComboboxId");

        var store = Ext.getCmp("VideoToGridId").getStore();

        if (comboValue.getValue() === null) {
            store.getProxy().setExtraParam("album", "vdefault");
        } else {
            store.getProxy().setExtraParam("album", comboValue.getValue());
        }
        store.save({
            success: function () {
                console.log("success!!");
                Ext.example.msg("Successfully saved !", "Album: " + comboValue.getValue() + "Record Count: " + store.getCount());
            },
            failure: function () {
                console.log("failed...");
            },
            callback: function () {
                console.log("calling callback");
            },
            scope: this
        });
    }
});