using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;
using LuckyMe.CMS.Common.Models.ViewModels;
using LuckyMe.CMS.Web.Models;
using Newtonsoft.Json.Linq;
using LuckyMe.CMS.Common.Models.ViewModels.fb;

namespace LuckyMe.CMS.Web.Clients
{
    public class LuckyMeClient
    {
        private const string AccessTokenKey = "access_token";

        private static readonly Uri BaseAddress = new Uri(WebConfigurationManager.AppSettings["Base_Address"]);

        public string AccessToken { get; set; }

        private readonly List<MediaTypeFormatter> _formatters = new List<MediaTypeFormatter>()
        {
            new JsonMediaTypeFormatter(),
            new XmlMediaTypeFormatter()
        };

        #region Login & Register

        // Register - working
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

        // Login    - working
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
                    return (string) result[AccessTokenKey];
                }
            }
        }

        #endregion

        #region Security

        // User Validation    - working but attribute needs to be tested
        public async Task<bool> ValidateUserAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                var response = httpClient.GetAsync("/api/Account/IsAuthenticated").Result;
                //response.EnsureSuccessStatusCode();
                var userinfo = await response.Content.ReadAsAsync<bool>();
                return userinfo;
            }
        }

        #endregion

        #region External logins

        public async Task<string> InsertExternalLoginUserClaim(string provider, string code)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                var model = new UserClaimsBindingModel {ClaimType = provider, ClaimValue = code};
                using (
                    var response =
                        await
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/InsertUserClaim"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        public async Task<string> UpdateExternalLoginUserClaim(string provider, string code)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                var model = new UserClaimsBindingModel {ClaimType = provider, ClaimValue = code};
                using (
                    var response =
                        await
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/UpdateUserClaim"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        public async Task<string> DeleteExternalLoginUserClaim(string provider, string code)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                var model = new UserClaimsBindingModel {ClaimType = provider, ClaimValue = code};
                using (
                    var response =
                        await
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/DeleteUserClaim"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        #endregion

        #region DashBoard

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

        #endregion

        #region Account Settings

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

        public async Task<string> UpdateProfileAsync(ProfileViewModel model)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                using (
                    var response =
                        await
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/AddExternalLoginToUser"), model))
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
                using (
                    var response =
                        await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Account/ChangePassword"), model))
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
                using (
                    var response =
                        await
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/AddExternalLoginToUser"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        #endregion

        #region Facebook Methods

        //GetProfile
        public async Task<FacebookProfileViewModel> GetFacebookProfileAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/FaceBook/GetProfile"))
                {
                    var result = await response.Content.ReadAsAsync<FacebookProfileViewModel>(_formatters);
                    return result;
                }
            }
        }

        //GetAllAlbums
        public async Task<List<FacebookAlbumViewModel>> GetAllFacebookAlbumsAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/FaceBook/GetAllAlbums"))
                {
                    var result = await response.Content.ReadAsAsync<List<FacebookAlbumViewModel>>(_formatters);
                    return result;
                }
            }
        }

        //GetAlbum
        public async Task<FacebookAlbumViewModel> GetFacebookAlbumAsync(string albumId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/FaceBook/GetAlbum?albumId=" + albumId))
                {
                    var result = await response.Content.ReadAsAsync<FacebookAlbumViewModel>(_formatters);
                    return result;
                }
            }
        }

        //GetAlbumPhotos
        public async Task<List<FacebookPhotoViewModel>> GetAlbumPhotosAsync(string albumId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/FaceBook/GetAlbumPhotos?albumId=" + albumId))
                {
                    var result = await response.Content.ReadAsAsync<List<FacebookPhotoViewModel>>(_formatters);
                    return result;
                }
            }
        }

        //GetAlbumPhoto
        public async Task<FacebookPhotoViewModel> GetAlbumPhotoAsync(string albumId, string photoId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (
                    var response =
                        await
                            httpClient.GetAsync("/api/FaceBook/GetAlbumPhoto?albumId=" + albumId + "&photoId=" +
                                                photoId))
                {
                    var result = await response.Content.ReadAsAsync<FacebookPhotoViewModel>(_formatters);
                    return result;
                }
            }
        }

        //GetAllVideos
        public async Task<List<FacebookVideoViewModel>> GetAllVideosAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/FaceBook/GetAllVideos"))
                {
                    var result = await response.Content.ReadAsAsync<List<FacebookVideoViewModel>>(_formatters);
                    return result;
                }
            }
        }

        //GetVideo

        public async Task<FacebookVideoViewModel> GetVideoAsync(string videoId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/FaceBook/GetVideo?videoId=" + videoId))
                {
                    var result = await response.Content.ReadAsAsync<FacebookVideoViewModel>(_formatters);
                    return result;
                }
            }
        }

        #endregion

        #region Blob methods

        //UploadPhotoToAlbum
        public async Task<string> UploadPhotoToAlbumAsync(BlobFileViewModel model)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);
                using (
                    var response =
                        await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Blob/UploadPhotoToAlbum"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        //UploadPhotoToAlbum
        public async Task<string> UploadPhotosToAlbumAsync(List<BlobFileViewModel> photoList)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);
                using (
                    var response =
                        await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Blob/UploadPhotosToAlbum"), photoList))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        //UploadVideoToAlbum
        public async Task<string> UploadVideoToAlbumAsync(BlobFileViewModel video)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);
                using (
                    var response =
                        await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Blob/UploadVideoToAlbum"), video))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        //UploadVideoToAlbum
        public async Task<string> UploadVideosToAlbumAsync(List<BlobFileViewModel> videoList)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);
                using (
                    var response =
                        await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Blob/UploadVideosToAlbum"), videoList))
                {
                    return response.StatusCode.ToString();
                }
            }
        }
        #endregion

    }
}