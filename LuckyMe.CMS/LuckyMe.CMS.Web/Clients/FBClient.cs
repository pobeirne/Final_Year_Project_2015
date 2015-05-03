using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using Facebook;

namespace LuckyMe.CMS.Web.Client
{
    public class FBClient
    {
        private readonly string _appId = WebConfigurationManager.AppSettings["Facebook_AppId"];
        private readonly Uri _redirectUri = new Uri("https://localhost:44301/ExternalAccount/FacebookAccountCallBack");
       

        public async Task<string> GetLoginUrl()
        {
            var client = new FacebookClient();
            var loginUrl = await TaskEx.Run( () => client.GetLoginUrl(new
            {
                client_id = _appId,
                redirect_uri = _redirectUri,
                response_type = "code",
                scope = GetUserSubscriptionFields()
            }));

            return loginUrl.AbsoluteUri;
        }

        protected virtual string[] GetUserSubscriptionFields()
        {
            return new[]
            {
                "public_profile",
                "email",
                "user_videos",
                "user_photos"
            };
        }
    }
}
