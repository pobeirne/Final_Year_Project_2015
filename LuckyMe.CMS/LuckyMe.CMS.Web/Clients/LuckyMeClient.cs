using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LuckyMe.CMS.Web.Models;
using Newtonsoft.Json.Linq;

namespace LuckyMe.CMS.Web.Clients
{
    public class LuckyMeClient
    {
        private static readonly string _accessTokenKey = "access_token";
        public Uri BaseAddress { get; set; }
        public string AccessToken { get; set; }

        private readonly List<MediaTypeFormatter> _formatters = new List<MediaTypeFormatter>()
        {
            new JsonMediaTypeFormatter(),
            new XmlMediaTypeFormatter()
        };

        //#1 Register - working
        public async Task<string> RegisterAsync(RegisterModel model)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                using (
                    var response =
                        await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Account/Register"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        //#2 Login    - working
        public async Task<string> LoginAsync(LoginModel model)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;

                using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(BaseAddress, "/token")))
                {
                    request.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
                    {
                        {"grant_type", "password"},
                        {"username", model.Email},
                        {"password", model.Password}
                    });

                    var response = await httpClient.SendAsync(request);
                    var result = await response.Content.ReadAsAsync<JObject>(_formatters);
                    return (string) result[_accessTokenKey];
                }
            }
        }

        //#3 User Validation    - working but attribute needs to be verified
        public async Task<bool> ValidateUserAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                var response = httpClient.GetAsync("/api/User/IsAuthenticated").Result;
                response.EnsureSuccessStatusCode();
                var userinfo = await response.Content.ReadAsAsync<bool>();
                return userinfo;
            }
        }













        
        
        public async Task<string> AddExternalLoginTokenToUser(string provider, string code)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                var model = new UserExternalLoginsModel { LoginProvider = provider, ProviderKey = code };
                using (var response = await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/AddExternalLoginToUser"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }
        
        public async Task<UserInfoViewModel> GetCurrentUserInfoAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/User/UserInfo"))
                {
                    response.EnsureSuccessStatusCode();
                    var userinfo = await response.Content.ReadAsAsync<UserInfoViewModel>();
                    return userinfo;
                }
            }
        }

        //Overview
        
        public async Task<OverviewViewModel> GetUserOverviewAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/User/Overview"))
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsAsync<OverviewViewModel>();
                    return result;
                }
            }
        }

        public async Task<ProfileViewModel> GetUserProfileAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/User/Profile"))
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsAsync<ProfileViewModel>();
                    return result;
                }
            }
        }

        public async Task<AccountViewModel> GetUserAccountAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/User/Account"))
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsAsync<AccountViewModel>();
                    return result;
                }
            }
        }
        
        public async Task<ChangePasswordModel> GetUserCurrentPasswordAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/User/UserInfo"))
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsAsync<ChangePasswordModel>();
                    return result;
                }
            }
        }
        
        //Account
        
        public async Task<string> UpdateProfileAsync(ProfileViewModel model)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                using (var response = await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/AddExternalLoginToUser"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        public async Task<string> UpdatePasswordAsync(ChangePasswordModel model)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                using (var response = await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/AddExternalLoginToUser"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        public async Task<string> DeleteAccountAsync(AccountViewModel model)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                using (var response = await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/AddExternalLoginToUser"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }
        
    }
}