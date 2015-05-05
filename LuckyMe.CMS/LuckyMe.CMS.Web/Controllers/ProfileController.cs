using System.Threading.Tasks;
using System.Web.Mvc;
using LuckyMe.CMS.Web.Clients;
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
            ProfileViewModel model = await _client.GetUserProfileAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(ProfileViewModel model)
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