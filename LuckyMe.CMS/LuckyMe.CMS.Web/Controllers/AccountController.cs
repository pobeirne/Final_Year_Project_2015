using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using LuckyMe.CMS.Web.Client;
using LuckyMe.CMS.Web.Clients;
using LuckyMe.CMS.Web.Filters;
using LuckyMe.CMS.Web.Models;

namespace LuckyMe.CMS.Web.Controllers
{
    
    public class AccountController : Controller
    {
        private UserSession _curruser;

        private readonly LuckyMeClient _client;
        private readonly FBClient _myfbClient;
        private static Uri _baseAddressUri;
        
        public AccountController()
        {
            _curruser = new UserSession();
            _client = new LuckyMeClient();
            _myfbClient = new FBClient();
            _baseAddressUri = new Uri("http://localhost:4797");
        }


        //#1 Register

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
            _client.BaseAddress = _baseAddressUri;
            var result = await _client.RegisterAsync(model);
            if (result == "OK")
            {
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", result);
            return View(model);
        }

        #endregion

        //#2 Login

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
            _client.BaseAddress = _baseAddressUri;
            var bearertoken = await _client.LoginAsync(model);
            if (bearertoken != "")
            {
                _client.AccessToken = bearertoken;
                _curruser = new UserSession {Token = bearertoken, BaseAddress = _baseAddressUri};
                Session["UserSession"] = _curruser;
                return RedirectToAction("UserOverview", "Dashboard");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        #endregion

        //#3 Logout

        #region Logout

        [HttpPost]
        [UserValidation]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();
            _curruser = (UserSession) Session["UserSession"];
            if (_curruser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("UserOverview", "Dashboard");
        }

        #endregion

        


        [HttpGet]
        [UserValidation]
        public async Task<ActionResult> ChangePassword()
        {
            _curruser = (UserSession)Session["UserSession"];
            _client.BaseAddress = _curruser.BaseAddress;
            _client.AccessToken = _curruser.Token;
            ChangePasswordModel model = await _client.GetUserCurrentPasswordAsync();
            return View(model);
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

            _curruser = (UserSession)Session["UserSession"];
            _client.BaseAddress = _curruser.BaseAddress;
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
            _curruser = (UserSession)Session["UserSession"];
            _client.BaseAddress = _curruser.BaseAddress;
            _client.AccessToken = _curruser.Token;
            AccountViewModel account = await _client.GetUserAccountAsync();
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

            _curruser = (UserSession)Session["UserSession"];
            _client.BaseAddress = _curruser.BaseAddress;
            _client.AccessToken = _curruser.Token;
            var result = await _client.DeleteAccountAsync(model);

            if (result == "OK")
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid attempt.");
            return View(model);
        }

       
        
        //External login


        [HttpGet]
        [UserValidation]
        public async Task<ActionResult> ManageExternalLogin()
        {
            ViewBag.HasExternalLogin = true;
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
            return RedirectToAction("ManageExternalLogin", "Account");
        }
        






        [HttpPost]
        [UserValidation]
        public async Task<ActionResult> FacebookAccountLogin()
        {
            if (string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                var callbackUrl = await _myfbClient.GetLoginUrl();
                return Redirect(callbackUrl);
            }
            //Some Error Display needed
            return RedirectToAction("UserOverview", "Dashboard");
        }

        [HttpPost]
        [UserValidation]
        public async Task<ActionResult> FacebookAccountCallBack()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                _curruser = (UserSession)Session["UserSession"];
                _client.BaseAddress = _curruser.BaseAddress;
                _client.AccessToken = _curruser.Token;
                var response = await _client.AddExternalLoginTokenToUser("Facebook", Request.QueryString["code"]);

                if (response == "OK")
                {
                    return RedirectToAction("UserOverview", "Dashboard");
                }
            }
            return RedirectToAction("index", "Home");
        }
    }
}