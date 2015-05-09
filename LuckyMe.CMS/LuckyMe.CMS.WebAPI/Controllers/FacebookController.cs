using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using LuckyMe.CMS.Service.Services.Interfaces;
using LuckyMe.CMS.WebAPI.Providers;
using Microsoft.AspNet.Identity;

namespace LuckyMe.CMS.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Facebook")]
    public class FacebookController : ApiController
    {
        private readonly IUserService _userservice;

        private readonly string _appSecret;

        public FacebookController()
        {
        }

        public FacebookController(IUserService userservice)
        {
            _userservice = userservice;
            _appSecret = ConfigurationManager.AppSettings["Facebook_AppSecret"];
        }

        [Route("GetProfile")]
        public async Task<IHttpActionResult> GetFacebookProfileAsync()
        {
            //var user = await _userservice.GetUserByIdAsync("16cc730a-6db4-416c-8f8a-b8bad47f34ae");
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var firstOrDefault = user.UserClaims.FirstOrDefault(x => x.ClaimType == "FacebookAccessToken");

            if (firstOrDefault == null) return BadRequest("No Facebook Token exists or is Invalid");

            var fbAccessToken = firstOrDefault.ClaimValue;

            var graph = new FacebookGraph(_appSecret);

            var facebookProfile = await graph.GetProfileAsync(fbAccessToken);

            return Ok(facebookProfile);
        }

        [Route("GetAllAlbums")]
        public async Task<IHttpActionResult> GetAllAlbumsAsync()
        {
            //var user = await _userservice.GetUserByIdAsync("16cc730a-6db4-416c-8f8a-b8bad47f34ae");
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var firstOrDefault = user.UserClaims.FirstOrDefault(x => x.ClaimType == "FacebookAccessToken");

            if (firstOrDefault == null) return BadRequest("No Facebook Token exists or is Invalid");

            var fbAccessToken = firstOrDefault.ClaimValue;

            var graph = new FacebookGraph(_appSecret);

            var albums = await graph.GetAlbumsAsync(fbAccessToken);

            return Ok(albums);
        }

        [Route("GetAlbum")]
        public async Task<IHttpActionResult> GetAlbumAsync([FromUri] string albumId)
        {
            //var user = await _userservice.GetUserByIdAsync("16cc730a-6db4-416c-8f8a-b8bad47f34ae");
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var firstOrDefault = user.UserClaims.FirstOrDefault(x => x.ClaimType == "FacebookAccessToken");

            if (firstOrDefault == null) return BadRequest("No Facebook Token exists or is Invalid");

            var fbAccessToken = firstOrDefault.ClaimValue;

            var graph = new FacebookGraph(_appSecret);

            var album = await graph.GetAlbumAsync(fbAccessToken, albumId);

            return Ok(album);
        }

        [Route("GetAlbumPhotos")]
        public async Task<IHttpActionResult> GetAlbumPhotosAsync([FromUri] string albumId)
        {
            //var user = await _userservice.GetUserByIdAsync("16cc730a-6db4-416c-8f8a-b8bad47f34ae");
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var firstOrDefault = user.UserClaims.FirstOrDefault(x => x.ClaimType == "FacebookAccessToken");

            if (firstOrDefault == null) return BadRequest("No Facebook Token exists or is Invalid");

            var fbAccessToken = firstOrDefault.ClaimValue;

            var graph = new FacebookGraph(_appSecret);

            var photos = await graph.GetAlbumPhotosAsync(fbAccessToken, albumId);

            return Ok(photos);
        }

        [Route("GetAlbumPhoto")]
        public async Task<IHttpActionResult> GetAlbumPhotoAsync([FromUri] string albumId, [FromUri] string photoId)
        {
            //var user = await _userservice.GetUserByIdAsync("16cc730a-6db4-416c-8f8a-b8bad47f34ae");
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var firstOrDefault = user.UserClaims.FirstOrDefault(x => x.ClaimType == "FacebookAccessToken");

            if (firstOrDefault == null) return BadRequest("No Facebook Token exists or is Invalid");

            var fbAccessToken = firstOrDefault.ClaimValue;

            var graph = new FacebookGraph(_appSecret);

            var photo = await graph.GetAlbumPhotoAsync(fbAccessToken, albumId, photoId);

            return Ok(photo);
        }

        [Route("GetAllVideos")]
        public async Task<IHttpActionResult> GetAllVideosAsync()
        {
            //var user = await _userservice.GetUserByIdAsync("16cc730a-6db4-416c-8f8a-b8bad47f34ae");
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var firstOrDefault = user.UserClaims.FirstOrDefault(x => x.ClaimType == "FacebookAccessToken");

            if (firstOrDefault == null) return BadRequest("No Facebook Token exists or is Invalid");

            var fbAccessToken = firstOrDefault.ClaimValue;

            var graph = new FacebookGraph(_appSecret);

            var videos = await graph.GetVideosAsync(fbAccessToken);

            return Ok(videos);
        }

        [Route("GetVideo")]
        public async Task<IHttpActionResult> GetVideoAsync([FromUri] string videoId)
        {
            //var user = await _userservice.GetUserByIdAsync("16cc730a-6db4-416c-8f8a-b8bad47f34ae");
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var firstOrDefault = user.UserClaims.FirstOrDefault(x => x.ClaimType == "FacebookAccessToken");

            if (firstOrDefault == null) return BadRequest("No Facebook Token exists or is Invalid");

            var fbAccessToken = firstOrDefault.ClaimValue;

            var graph = new FacebookGraph(_appSecret);

            var video = await graph.GetVideoAsync(fbAccessToken, videoId);

            return Ok(video);
        }
    }
}
    

