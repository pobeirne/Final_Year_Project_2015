using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using Facebook;

namespace LuckyMe.CMS.Web.Clients
{
    public class FbClient
    {
        private readonly string _appId = WebConfigurationManager.AppSettings["Facebook_AppId"];
        private readonly string _appSecret = WebConfigurationManager.AppSettings["Facebook_AppSecret"];
        private readonly Uri _redirectUri = new Uri("https://localhost:44301/Account/FacebookAccountCallBack");
       

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


        public async Task<string> GetAccessToken(string code)
        {
            var client = new FacebookClient();

            dynamic result = await TaskEx.Run(() => client.Post("oauth/access_token", new
            {
                client_id = _appId,
                client_secret = _appSecret,
                redirect_uri = "https://localhost:44301/Account/FacebookAccountCallBack",
                code = code
            }));

            return result.access_token;
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
