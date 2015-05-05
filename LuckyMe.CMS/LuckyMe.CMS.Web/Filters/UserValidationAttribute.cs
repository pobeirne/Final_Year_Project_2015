using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LuckyMe.CMS.Web.Clients;
using LuckyMe.CMS.Web.Models;

namespace LuckyMe.CMS.Web.Filters
{
    public class UserValidationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session != null)
            {
                var user = (UserSession) httpContext.Session["UserSession"];
                if (user.Token != null)
                {
                    var client = new LuckyMeClient()
                    {
                        AccessToken = user.Token
                    };

                    if (client.ValidateUserAsync().Result)
                    {
                        return true;
                    }
                }
             }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Error",
                        action = "Unauthorised"
                    })
                );
        }
    }
}