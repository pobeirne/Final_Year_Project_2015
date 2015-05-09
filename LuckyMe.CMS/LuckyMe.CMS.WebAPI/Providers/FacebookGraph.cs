using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facebook;
using LuckyMe.CMS.Common.Extensions;
using LuckyMe.CMS.Common.Models.ViewModels.fb;

namespace LuckyMe.CMS.WebAPI.Providers
{
    public class FacebookGraph
    {
        private FacebookClient _client;
        private readonly string _appsecret;

        public FacebookGraph(string appsecret)
        {
            _appsecret = appsecret;
        }

        public async Task<FacebookProfileViewModel> GetProfileAsync(string accesstoken)
        {
            try
            {
                if (accesstoken == null) return null;

                _client = new FacebookClient(accesstoken);
                var appsecretProof = accesstoken.GenerateAppSecretProof(_appsecret);
                dynamic profile = await _client.GetTaskAsync("me?".GraphApiCall(appsecretProof));
                dynamic profileImg =
                    await
                        _client.GetTaskAsync(
                            "{0}/picture?width=1000&height=1000&redirect=false".GraphApiCall((string) profile.id,
                                appsecretProof));

                var facebookProfile = DynamicExtension.ToStatic<FacebookProfileViewModel>(profile);
                facebookProfile.ImageUrl = profileImg.data.url;

                return facebookProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<FacebookAlbumViewModel>> GetAlbumsAsync(string accesstoken)
        {
            try
            {
                if (accesstoken == null) return null;

                _client = new FacebookClient(accesstoken);
                var appsecretProof = accesstoken.GenerateAppSecretProof(_appsecret);
                dynamic albums = await _client.GetTaskAsync(
                    string.Format("me/albums?fields=id,name, count, link," +
                                  "picture, updated_time")
                        .GraphApiCall(appsecretProof), null);

                var albumList = new List<FacebookAlbumViewModel>();
                foreach (var album in albums.data)
                {
                    albumList.Add(DynamicExtension.ToStatic<FacebookAlbumViewModel>(album));
                }
                return albumList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FacebookAlbumViewModel> GetAlbumAsync(string accesstoken, string albumId)
        {
            try
            {
                if (accesstoken == null) return null;

                var albums = await GetAlbumsAsync(accesstoken);
                var album = albums.FirstOrDefault(x => x.Id == albumId);

                return album;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<FacebookPhotoViewModel>> GetAlbumPhotosAsync(string accesstoken, string albumId)
        {
            try
            {
                if (accesstoken == null) return null;

                _client = new FacebookClient(accesstoken);
                var appsecretProof = accesstoken.GenerateAppSecretProof(_appsecret);


                dynamic photos = await _client.GetTaskAsync(
                    (string.Format("{0}/photos", albumId)
                     + "?fields=id,picture,name,source&limit=25")
                        .GraphApiCall(appsecretProof));

                var photoList = HydratePhotoList(photos);

                return photoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FacebookPhotoViewModel> GetAlbumPhotoAsync(string accesstoken, string albumId, string photoId)
        {
            try
            {
                if (accesstoken == null) return null;

                var photos = await GetAlbumPhotosAsync(accesstoken, albumId);
                var photo = photos.FirstOrDefault(x => x.Id == photoId);

                return photo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<FacebookVideoViewModel>> GetVideosAsync(string accesstoken)
        {
            try
            {
                if (accesstoken == null) return null;

                _client = new FacebookClient(accesstoken);
                var appsecretProof = accesstoken.GenerateAppSecretProof(_appsecret);


                dynamic videos = await _client.GetTaskAsync(
                    string.Format("me/videos?fields=id,name,description," +
                                  "picture,embed_html,source,created_time")
                        .GraphApiCall(appsecretProof), null);

                var videoList = HydrateVideoList(videos);

                return videoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FacebookVideoViewModel> GetVideoAsync(string accesstoken, string videoId)
        {
            try
            {
                if (accesstoken == null) return null;

                var videos = await GetVideosAsync(accesstoken);
                var video = videos.FirstOrDefault(x => x.Id == videoId);

                return video;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<FacebookPhotoViewModel> HydratePhotoList(dynamic result)
        {
            var photoList = new List<FacebookPhotoViewModel>();
            foreach (var photo in result.data)
            {
                photoList.Add(DynamicExtension.ToStatic<FacebookPhotoViewModel>(photo));
            }
            return photoList;
        }

        private static List<FacebookVideoViewModel> HydrateVideoList(dynamic result)
        {
            var videoList = new List<FacebookVideoViewModel>();
            foreach (var video in result.data)
            {
                videoList.Add(DynamicExtension.ToStatic<FacebookVideoViewModel>(video));
            }
            return videoList;
        }
    }
}