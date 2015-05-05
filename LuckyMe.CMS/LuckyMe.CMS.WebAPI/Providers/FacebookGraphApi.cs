using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Facebook;
using LuckyMe.CMS.WebAPI.Extensions;
using LuckyMe.CMS.WebAPI.Models;

namespace LuckyMe.CMS.WebAPI.Providers
{
    public class FacebookGraphApi
    {
        private string _accessToken = "access_token";
        
        public async Task<FacebookProfileViewModel> GetUserProfile()
        {
            const string accessToken = "CAAURYutqy2sBALUEjoFklg0QEvuxwph9ytlzZCKNYmK00xMZBYtHZCcA3jHIRRcZCF7lsWC0t4cOb4ZAgOJA9R8Aag2Qe9N7q9OxpmsWWaGmWtz6pdM4XSTAQFSzPo6FNezZCWXUbcnrzo6sh71IwOWKtOFZBnyqHBu6ZAAvdrSZBaiEdCYn19CH9PxukF3DDplLgd63NoKf2H6gERv3B3LYG";

            try
            {
                var appsecretProof = accessToken.GenerateAppSecretProof();
                var fb = new FacebookClient(accessToken);

                //Get current user's profile
                dynamic myInfo = await fb.GetTaskAsync("me?fields=first_name,last_name,link,locale,email,name".GraphApiCall(appsecretProof));

                //get current picture
                dynamic profileImgResult = await fb.GetTaskAsync("{0}/picture?width=200&height=200&redirect=false".GraphApiCall((string)myInfo.id, appsecretProof));

                //Hydrate FacebookProfileViewModel with Graph API results
                var facebookProfile = DynamicExtension.ToStatic<FacebookProfileViewModel>(myInfo);
                facebookProfile.ImageURL = profileImgResult.data.url;
                return facebookProfile;

            }
            catch (Exception ex)
            {
              
                throw new Exception(ex.Message);
            }
        }
    }
}