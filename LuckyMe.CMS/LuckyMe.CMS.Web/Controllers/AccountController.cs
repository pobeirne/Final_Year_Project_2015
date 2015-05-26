using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using LuckyMe.CMS.Common.Models.ViewModels;
using LuckyMe.CMS.Web.ClientHelpers;
using LuckyMe.CMS.Web.Filters;
using LuckyMe.CMS.Web.Models;

namespace LuckyMe.CMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserSession _curruser;

        private readonly LuckyMeClient _client;
        private readonly FaceBookClient _myfbClient;
        private static Uri _baseAddress;

        public AccountController()
        {
            _curruser = new UserSession();
            _client = new LuckyMeClient();
            _myfbClient = new FaceBookClient();
            _baseAddress = new Uri(WebConfigurationManager.AppSettings["Base_Address"]);
        }


        // Register -working

        #region Register

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //_client.BaseAddress = _baseAddressUri;
            var result = await _client.RegisterAsync(model);
            if (result == "OK")
            {
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", result);
            return View(model);
        }

        #endregion

        // Login -working

        #region Login

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // _client.BaseAddress = _baseAddressUri;
            var bearertoken = await _client.LoginAsync(model);
            if (bearertoken != null)
            {
                _client.AccessToken = bearertoken;
                _curruser = new UserSession {Token = bearertoken, BaseAddress = _baseAddress};
                Session["UserSession"] = _curruser;
                return RedirectToAction("UserOverview", "Dashboard");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        #endregion

        // Logout -working

        #region Logout

        [HttpPost]
        [UserValidation]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();
            _curruser = (UserSession) Session["UserSession"];
            return _curruser == null
                ? RedirectToAction("Login", "Account")
                : RedirectToAction("UserOverview", "Dashboard");
        }

        #endregion

        // Account settings

        #region Account settings

        [HttpGet]
        [UserValidation]
        public ViewResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [UserValidation]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            var result = await _client.UpdatePasswordAsync(model);

            if (result == "OK")
            {
                return RedirectToAction("ChangePassword", "Account");
            }

            ModelState.AddModelError("", "Invalid attempt.");
            return View(model);
        }

        [HttpGet]
        [UserValidation]
        public async Task<ActionResult> DeleteAccount()
        {
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            var account = await _client.GetUserAccountAsync();
            return View(account);
        }

        [HttpPost]
        [UserValidation]
        public async Task<ActionResult> DeleteAccount(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            var result = await _client.DeleteAccountAsync(model);

            if (result == "OK")
            {
                Session.Clear();
                _curruser = (UserSession) Session["UserSession"];
                if (_curruser == null)
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            ModelState.AddModelError("", "Invalid attempt.");
            return View(model);
        }

        #endregion

        // External login

        #region External logins

        [HttpGet]
        [UserValidation]
        public async Task<ActionResult> ManageExternalLogin()
        {
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            var curruser = await _client.GetCurrentUserInfoAsync();
           
            ViewBag.HasExternalLogin = curruser.HasFacebookCliam;
            
       
            return View();
        }

        [HttpPost]
        [UserValidation]
        public async Task<ActionResult> RefreshExternalLogin()
        {
            return RedirectToAction("ManageExternalLogin", "Account");
        }

        [HttpPost]
        [UserValidation]
        public async Task<ActionResult> DeleteExternalLogin()
        {
            _curruser = (UserSession)Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            var deleted = await _client.DeleteUserClaim("FacebookAccessToken");
            return RedirectToAction("ManageExternalLogin", "Account");
        }

        #endregion

        // Facebook login

        #region Facebook login

        [UserValidation]
        public async Task<ActionResult> FacebookAccountLogin()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                return RedirectToAction("ManageExternalLogin", "Account");
            var callbackUrl = await _myfbClient.GetLoginUrl();
            return Redirect(callbackUrl);
        }


        [UserValidation]
        public async Task<ActionResult> FacebookAccountCallBack()
        {
            if (string.IsNullOrEmpty(Request.QueryString["code"]))
                return RedirectToAction("ManageExternalLogin", "Account");

            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            var token = await _myfbClient.GetAccessToken(Request.QueryString["code"]);

            var response = await _client.InsertUserClaim("FacebookAccessToken", token);
            if (response != "OK") RedirectToAction("ManageExternalLogin", "Account");
            return RedirectToAction("ManageExternalLogin", "Account");
        }

        #endregion
    }
}