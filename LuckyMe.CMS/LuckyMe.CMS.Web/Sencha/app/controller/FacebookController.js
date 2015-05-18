Ext.define("Facebook.controller.FacebookController",
{
    extend: "Ext.app.Controller",

    models: [
        "Facebook.model.FacebookPhotoModel",
        "Facebook.model.FacebookVideoModel"
    ],

    stores: [
        "Facebook.store.FacebookPhotoStore",
        "Facebook.store.FacebookPhotoViewStore",
        "Facebook.store.FacebookVideoStore",
        "Facebook.store.FacebookVideoViewStore"
    ],

    views: [
        "Facebook.view.Facebook.PhotoGrid",
        "Facebook.view.Facebook.PhotoView",
        "Facebook.view.Facebook.VideoGrid",
        "Facebook.view.Facebook.VideoView"
    ],

    init: function() {

        this.control({

            }
        );
    }
});