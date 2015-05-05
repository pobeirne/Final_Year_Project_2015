using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using LuckyMe.CMS.Service.Services.Interfaces;
using LuckyMe.CMS.WebAPI.Providers;

namespace LuckyMe.CMS.WebAPI.Controllers
{
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

                var graph = new FacebookGraph(_appSecret);

                var facebookProfile = await graph.GetProfileAsync(fbAccessToken);

                var albumsTest = await graph.GetAlbumsAsync(fbAccessToken);

                var albumPhotos = await graph.GetAlbumPhotosAsync(fbAccessToken, albumsTest[0].Id);

                var videos = await graph.GetVideosAsync(fbAccessToken);

                return Ok(facebookProfile);
            }

            return NotFound();
        }
    }
}
    

