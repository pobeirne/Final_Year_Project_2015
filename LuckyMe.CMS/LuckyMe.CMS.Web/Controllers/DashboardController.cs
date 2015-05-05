using System.Threading.Tasks;
using System.Web.Mvc;
using LuckyMe.CMS.Web.Clients;
using LuckyMe.CMS.Web.Filters;
using LuckyMe.CMS.Web.Models;

namespace LuckyMe.CMS.Web.Controllers
{
    [UserValidation]
    public class DashboardController : Controller
    {
        private UserSession _curruser;
        private readonly LuckyMeClient _client;

        public DashboardController()
        {
            _curruser = new UserSession();
            _client = new LuckyMeClient();
        }

        public async Task<ActionResult> UserOverview()
        {
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            OverviewViewModel overview = await _client.GetUserOverviewAsync();
            return View(overview);
        }

        public async Task<ActionResult> UserProfile()
        {
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            ProfileViewModel profile = await _client.GetUserProfileAsync();
            return View(profile);
        }

        public async Task<ActionResult> UserAccount()
        {
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            AccountViewModel account = await _client.GetUserAccountAsync();
            return View(account);
        }
    }
}