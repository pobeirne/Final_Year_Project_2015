using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using LuckyMe.CMS.Entity.DTO;
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
       
        public UserController()
        {
        }

        public UserController(IUserService userservice)
        {
            _userservice = userservice;
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

            return new CurrentUserInfo()
            {
                Name = user.UserName,
                Email = user.Email,
                HasRegistered = User.Identity.IsAuthenticated
            };
        }






        // User Claims 

        #region User Claims

        [Route("InsertUserClaim")]
        public async Task<IHttpActionResult> InsertUserClaimAsync(UserClaimsBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new UserClaimDTO
            {
                UserId = User.Identity.GetUserId(),
                ClaimType = model.ClaimType,
                ClaimValue = model.ClaimValue
            };

            var result = await _userservice.InsertUserClaimAsync(entry);

            if (result) return Ok();
            return BadRequest();
        }

        [Route("UpdateUserClaim")]
        public async Task<IHttpActionResult> UpdateUserClaimAsync(UserClaimsBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new UserClaimDTO
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
        public async Task<IHttpActionResult> DeleteUserClaimAsync(UserClaimsBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new UserClaimDTO
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
            return new OverviewViewModel()
            {
                Email = "pobeirne1@hotmail.com"
            };
        }

        [Route("Profile")]
        public async Task<ProfileViewModel> GetUserProfileAsync()
        {
            return new ProfileViewModel()
            {
                Email = "pobeirne2@hotmail.com"
            };
        }

        [Route("Account")]
        public async Task<AccountViewModel> GetUserAccountAsync()
        {
            var fb = new FacebookGraphApi();
            var test = await fb.GetUserProfile();

            var check = "";

            return new AccountViewModel()
            {
                Email = "pobeirne3@hotmail.com"
            };
        }
        
    }
}




