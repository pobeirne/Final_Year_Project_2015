using System.Threading.Tasks;
using System.Web.Mvc;
using LuckyMe.CMS.Common.Models.ViewModels;
using LuckyMe.CMS.Web.ClientHelpers;
using LuckyMe.CMS.Web.Filters;
using LuckyMe.CMS.Web.Models;

namespace LuckyMe.CMS.Web.Controllers
{
    [UserValidation]
    public class ProfileController : Controller
    {
        private UserSession _curruser;
        private readonly LuckyMeClient _client;

        public ProfileController()
        {
            _curruser = new UserSession();
            _client = new LuckyMeClient();
        }

        [HttpGet]
        public async Task<ActionResult> EditProfile()
        {
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            var result = await _client.UpdateProfileAsync(model);

            if (result == "OK")
            {
                return RedirectToAction("EditProfile", "Profile");
            }

            ModelState.AddModelError("", "Invalid attempt.");
            return View(model);
        }
        
    }
}