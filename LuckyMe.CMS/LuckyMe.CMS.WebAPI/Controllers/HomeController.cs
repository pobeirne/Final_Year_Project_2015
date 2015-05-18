using System.Web.Mvc;

namespace LuckyMe.CMS.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
