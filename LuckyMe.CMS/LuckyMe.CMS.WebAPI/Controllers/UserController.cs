using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using LuckyMe.CMS.Entity.DTO;
using LuckyMe.CMS.Service.Services.Interfaces;
using LuckyMe.CMS.WebAPI.Models;
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
        
        [Route("IsAuthenticated")]
        public bool GetUserAuthentication()
        {
            return User.Identity.IsAuthenticated;
        }
        
        [Route("UserInfo")]
        public async Task<CurrentUserInfo> GetUserInfo()
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
        
        [Route("AddExternalLoginToUser")]
        public IHttpActionResult AddExternalLoginToUser(UserProviderBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new UserProviderDTO
            {
                UserId = User.Identity.GetUserId(),
                LoginProvider = model.LoginProvider,
                ProviderKey = model.ProviderKey
            };

            var result = _userservice.InsertUserExternalLoginEntry(entry);

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        










        [Route("Overview")]
        public async Task<OverviewViewModel> GetUserOverview()
        {
            return new OverviewViewModel()
            {
                Email = "pobeirne1@hotmail.com"
            };
        }

        [Route("Profile")]
        public async Task<ProfileViewModel> GetUserProfile()
        {
            return new ProfileViewModel()
            {
                Email = "pobeirne2@hotmail.com"
            };
        }

        [Route("Account")]
        public async Task<AccountViewModel> GetUserAccount()
        {
            return new AccountViewModel()
            {
                Email = "pobeirne3@hotmail.com"
            };
        }
        
    }
}




