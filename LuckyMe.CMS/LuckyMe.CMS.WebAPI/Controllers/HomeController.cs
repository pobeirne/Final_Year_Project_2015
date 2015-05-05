using System.Web.Mvc;

namespace LuckyMe.CMS.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
