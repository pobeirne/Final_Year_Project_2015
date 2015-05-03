using System.Web.Mvc;
using LuckyMe.CMS.Service.Services.Interfaces;

namespace LuckyMe.CMS.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userservice;

        public HomeController()
        {
        }

        public HomeController(IUserService userservice)
        {
            _userservice = userservice;
        }

        public ActionResult Index()
        {
            //var query = _userservice.GetAllUsers();
            //ViewBag.Message = "Count :" + query.Count();
            return View();
        }
    }
}
