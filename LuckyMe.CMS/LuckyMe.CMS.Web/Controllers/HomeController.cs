using System.Web.Mvc;

namespace LuckyMe.CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}