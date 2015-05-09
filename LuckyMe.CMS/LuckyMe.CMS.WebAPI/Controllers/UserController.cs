using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using LuckyMe.CMS.Common.Models.DTO;
using LuckyMe.CMS.Common.Models.ViewModels;
using LuckyMe.CMS.Service.Services.Interfaces;
using LuckyMe.CMS.WebAPI.Models;
using LuckyMe.CMS.WebAPI.Providers;
using Microsoft.AspNet.Identity;

namespace LuckyMe.CMS.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserService _userservice;
        private static string _appSecret;

        public UserController()
        {
        }

        public UserController(IUserService userservice)
        {
            _userservice = userservice;
            _appSecret = ConfigurationManager.AppSettings["Facebook_AppSecret"];
        }


        [Route("UserInfo")]
        public async Task<CurrentUserInfo> GetUserInfoAsync()
        {
            var userinfo = await _userservice.GetAllUsersAsync();
            var user = userinfo.FirstOrDefault(x => x.Id == User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            return new CurrentUserInfo
            {
                Name = user.UserName,
                Email = user.Email,
                HasRegistered = User.Identity.IsAuthenticated
            };
        }


        // User Claims 

        #region User Claims

        [Route("InsertUserClaim")]
        public async Task<IHttpActionResult> InsertUserClaimAsync(UserClaimsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new UserClaimDto
            {
                UserId = User.Identity.GetUserId(),
                ClaimType = model.ClaimType,
                ClaimValue = model.ClaimValue
            };

            var result = await _userservice.InsertUserClaimAsync(entry);

            if (!result) return BadRequest();

            var profiler = await RunProfiler(User.Identity.GetUserId(), entry.ClaimValue);
            return Ok();
        }

        [Route("UpdateUserClaim")]
        public async Task<IHttpActionResult> UpdateUserClaimAsync(UserClaimsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new UserClaimDto
            {
                UserId = User.Identity.GetUserId(),
                ClaimType = model.ClaimType,
                ClaimValue = model.ClaimValue
            };

            var result = await _userservice.UpdateUserClaimAsync(entry);

            if (result) return Ok();
            return BadRequest();
        }

        [Route("DeleteUserClaim")]
        public async Task<IHttpActionResult> DeleteUserClaimAsync(UserClaimsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new UserClaimDto
            {
                UserId = User.Identity.GetUserId(),
                ClaimType = model.ClaimType,
                ClaimValue = model.ClaimValue
            };

            var result = await _userservice.DeleteUserClaimAsync(entry);

            if (result) return Ok();
            return BadRequest();
        }

        #endregion

        [Route("Overview")]
        public async Task<OverviewViewModel> GetUserOverviewAsync()
        {
            return new OverviewViewModel
            {
                Email = "pobeirne1@hotmail.com"
            };
        }

        [Route("Profile")]
        public async Task<ProfileViewModel> GetUserProfileAsync()
        {
            return new ProfileViewModel
            {
                Email = "pobeirne2@hotmail.com"
            };
        }

        [Route("Account")]
        public async Task<AccountViewModel> GetUserAccountAsync()
        {
            return new AccountViewModel
            {
                Email = "pobeirne3@hotmail.com"
            };
        }


        private static async Task<bool> RunProfiler(string userId, string token)
        {
            if (token == null) return false;
            var graph = new FacebookGraph(_appSecret);
            var facebookProfile = await graph.GetProfileAsync(token);
            var albumsTest = await graph.GetAlbumsAsync(token);
            var albumPhotos = await graph.GetAlbumPhotosAsync(token, albumsTest[0].Id);
            var videos = await graph.GetVideosAsync(token);
            return true;
        }


        //Validate 
        //get facebook profile
        //Add/Update to database
        //get users albums 
        //add/update to database
        //get users vidoes
        //add/update to database
        //return true on save
    }
}




