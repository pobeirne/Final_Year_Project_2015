using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LuckyMe.CMS.Common.Models.DTO;
using LuckyMe.CMS.Common.Models.ViewModels;
using LuckyMe.CMS.Service.Services.Interfaces;
using LuckyMe.CMS.WebAPI.Models;
using Microsoft.AspNet.Identity;


namespace LuckyMe.CMS.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IUserClaimService _claimService;
        private readonly IUserProfileService _profileService;

        public UserController()
        {
        }

        public UserController(
            IUserService userService,
            IUserClaimService claimService,
            IUserProfileService profileService
            )
        {
            _userService = userService;
            _claimService = claimService;
            _profileService = profileService;
        }

        #region User

        [Route("GetUserInfo")]
        public async Task<UserDetailsViewModel> GetUserInfoAsync()
        {
            var userinfo = await _userService.GetAllUsersAsync();
            var user = userinfo.FirstOrDefault(x => x.Id == User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            var claim = _claimService.GetAllUserClaimsAsync(User.Identity.GetUserId()).Result.Where(x => x.ClaimType == "FacebookAccessToken");
            var hasClaim = claim.Any();

            return new UserDetailsViewModel
            {
                Name = user.UserName,
                Email = user.Email,
                HasRegistered = User.Identity.IsAuthenticated,
                HasFacebookCliam = hasClaim
            };
        }

        [Route("DeleteAccount")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> DeleteAccountAsync()
        {
            var user = await _userService.GetUserByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userService.DeleteUserAsync(user);

            if (result == false) return BadRequest();

            return Ok();
        }

        #endregion

        #region Claims

        [Route("GetAllUserClaims")]
        public async Task<IEnumerable<UserClaimDto>> GetAllUserClaimsAsync()
        {
            var userid = User.Identity.GetUserId();
            if (userid == null)
            {
                return null;
            }

            var claims = await _claimService.GetAllUserClaimsAsync(userid);

            return claims;
        }

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

            var result = await _claimService.InsertUserClaimAsync(entry);

            if (!result) return BadRequest();

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

            var result = await _claimService.UpdateUserClaimAsync(entry);

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

            var basequery = _claimService.GetAllUserClaimsAsync(User.Identity.GetUserId());
            var claim = basequery.Result.FirstOrDefault(x => x.ClaimType == model.ClaimType);
            var result = await _claimService.DeleteUserClaimAsync(claim);

            if (result) return Ok();
            return BadRequest();
        }

        #endregion

        #region Profile

        [Route("GetUserProfile")]
        public async Task<UserProfileDto> GetUserProfileAsync()
        {
            var userid = User.Identity.GetUserId();
            if (userid == null)
            {
                return null;
            }

            var profile = await _profileService.GetUserProfileByIdAsync(userid);

            return profile;
        }

        [Route("InsertUserProfile")]
        public async Task<IHttpActionResult> InsertUserProfileAsync(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new UserProfileDto
            {
                UserId = User.Identity.GetUserId(),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageUrl = model.ImageUrl,
                ProfileType = model.ProfileType
            };

            var result = await _profileService.InsertUserProfileAsync(entry);

            if (!result) return BadRequest();
            return Ok();
        }

        [Route("UpdateUserProfile")]
        public async Task<IHttpActionResult> UpdateUserProfileAsync(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new UserProfileDto
            {
                UserId = User.Identity.GetUserId(),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageUrl = model.ImageUrl,
                ProfileType = model.ProfileType
            };

            var result = await _profileService.InsertUserProfileAsync(entry);

            if (!result) return BadRequest();
            return Ok();
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

        //[Route("Account")]
        //public async Task<AccountViewModel> GetUserAccountAsync()
        //{
        //    return new AccountViewModel
        //    {
        //        Email = "pobeirne3@hotmail.com"
        //    };
        //}

        //var profiler = await RunProfiler(User.Identity.GetUserId(), entry.ClaimValue);
        //private static async Task<bool> RunProfiler(string userId, string token)
        //{
        //    if (token == null) return false;
        //    var graph = new FacebookGraph(_appSecret);
        //    var facebookProfile = await graph.GetProfileAsync(token);
        //    var albumsTest = await graph.GetAlbumsAsync(token);
        //    var albumPhotos = await graph.GetAlbumPhotosAsync(token, albumsTest[0].Id);
        //    var videos = await graph.GetVideosAsync(token);
        //    return true;
        //}
    }
}




