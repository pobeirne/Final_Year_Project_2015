using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Facebook;
using LuckyMe.CMS.Service.Services.Interfaces;
using LuckyMe.CMS.WebAPI.Extensions;
using LuckyMe.CMS.WebAPI.Models;

namespace LuckyMe.CMS.WebAPI.Controllers
{
    [RoutePrefix("api/Facebook")]
    public class FacebookController : ApiController
    {
        private readonly IUserService _userservice;

        public FacebookController()
        {
        }

        public FacebookController(IUserService userservice)
        {
            _userservice = userservice;
        }

        [Route("Profile")]
        public async Task<IHttpActionResult> GetFacebookProfileAsync()
        {
            var user = await _userservice.GetUserByIdAsync("16cc730a-6db4-416c-8f8a-b8bad47f34ae");
            //var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                var firstOrDefault = user.UserClaims.FirstOrDefault(x => x.ClaimType == "FacebookAccessToken");

                if (firstOrDefault == null) return NotFound();

                var fbAccessToken = firstOrDefault.ClaimValue;

                if (fbAccessToken == null) return NotFound();
                
                var graph = new FacebookGraph();

                var facebookProfile = await graph.GetProfileAsync(fbAccessToken);

                var albumsTest = await graph.GetAlbumsAsync(fbAccessToken);

                var albumPhotos = await graph.GetAlbumPhotosAsync(fbAccessToken, albumsTest[0].Id);

                var videos = await graph.GetVideosAsync(fbAccessToken);

                return Ok(facebookProfile);
            }

            return NotFound();
        }

        [Route("Profile")]
        public async Task<IHttpActionResult> InsertFacebookProfileAsync()
        {
            var user = await _userservice.GetUserByIdAsync("16cc730a-6db4-416c-8f8a-b8bad47f34ae");
    
            if (user != null)
            {

            }

            return NotFound();
        }


    }

    public class FacebookGraph
    {
        private FacebookClient _client;

        public async Task<FacebookProfileViewModel> GetProfileAsync(string accesstoken)
        {
            try
            {
                if (accesstoken == null) return null;

                _client = new FacebookClient(accesstoken);
                var appsecretProof = accesstoken.GenerateAppSecretProof();
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
                var appsecretProof = accesstoken.GenerateAppSecretProof();
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

        public async Task<List<FacebookPhotoViewModel>> GetAlbumPhotosAsync(string accesstoken, string albumId)
        {
            try
            {
                if (accesstoken == null) return null;

                _client = new FacebookClient(accesstoken);
                var appsecretProof = accesstoken.GenerateAppSecretProof();


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


        public async Task<List<FacebookVideoViewModel>> GetVideosAsync(string accesstoken)
        {
            try
            {
                if (accesstoken == null) return null;

                _client = new FacebookClient(accesstoken);
                var appsecretProof = accesstoken.GenerateAppSecretProof();


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
    

