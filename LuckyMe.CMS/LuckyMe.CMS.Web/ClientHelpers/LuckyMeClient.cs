using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;
using LuckyMe.CMS.Common.Models;
using LuckyMe.CMS.Common.Models.ViewModels;
using LuckyMe.CMS.Web.Models;
using Newtonsoft.Json.Linq;

namespace LuckyMe.CMS.Web.ClientHelpers
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

        public async Task<string> InsertUserClaim(string type, string value)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                var model = new UserClaimsBindingModel {ClaimType = type, ClaimValue = value};
                using (
                    var response =
                        await
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/InsertUserClaim"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        public async Task<string> UpdateUserClaim(string type, string value)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                var model = new UserClaimsBindingModel {ClaimType = type, ClaimValue = value};
                using (
                    var response =
                        await
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/UpdateUserClaim"), model))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        public async Task<string> DeleteUserClaim(string type)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                var model = new UserClaimsBindingModel {ClaimType = type, ClaimValue = ""};
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

        #region Account Settings

        public async Task<UserDetailsViewModel> GetCurrentUserInfoAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/User/GetUserInfo"))
                {
                    var userinfo = await response.Content.ReadAsAsync<UserDetailsViewModel>(_formatters);
                    return userinfo;
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
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/User/DeleteAccount"), model))
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

        //GetAllPhotos
        public async Task<List<FacebookPhotoViewModel>> GetAllPhotosAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/FaceBook/GetAllPhotos"))
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

        //GetAllBlobPhotos
        public async Task<List<FileInfoViewModel>> GetAllBlobPhotosAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/Blob/GetAllBlobPhotos"))
                {
                    var result = await response.Content.ReadAsAsync<List<FileInfoViewModel>>(_formatters);
                    return result;
                }
            }
        }

        //GetAllBlobVideos
        public async Task<List<FileInfoViewModel>> GetAllBlobVideosAsync()
        {
            try
            {

            
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/Blob/GetAllBlobVideos"))
                {
                    var result = await response.Content.ReadAsAsync<List<FileInfoViewModel>>(_formatters);
                    return result;
                }
            }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        //GetAllBlobFiles
        public async Task<List<UserBlobFile>> GetAllBlobFilesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/Blob/GetAllBlobFiles"))
                {
                    var result = await response.Content.ReadAsAsync<List<UserBlobFile>>(_formatters);
                    return result;
                }
            }
        }


        //UploadPhotoToAlbum
        public async Task<string> UploadPhotoToAlbumAsync(BlobFileViewModel photo)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);
                using (
                    var response =
                        await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Blob/UploadPhotoToAlbum"), photo))
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
                        await
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Blob/UploadPhotosToAlbum"), photoList)
                    )
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
                        await
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Blob/UploadVideosToAlbum"), videoList)
                    )
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        //RemovePhotoFromAlbum
        public async Task<string> RemovePhotoFromAlbumAsync(BlobFileViewModel photo)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);
                using (
                    var response =
                        await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Blob/RemovePhotoFromAlbum"), photo))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        //DeletePhotosFromAlbumAsync
        public async Task<string> DeletePhotosFromAlbumAsync(List<BlobFileViewModel> photos)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);
                using (
                    var response =
                        await
                            httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Blob/RemovePhotosFromAlbum"), photos))
                {
                    return response.StatusCode.ToString();
                }
            }
        }


        public async Task<string> DeleteVideoFromAlbumAsync(BlobFileViewModel video)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);
                using (
                    var response =
                        await httpClient.PostAsJsonAsync(new Uri(BaseAddress, "/api/Blob/RemoveVideoFromAlbum"), video))
                {
                    return response.StatusCode.ToString();
                }
            }
        }

        #endregion

        //not implemented

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
                    var result = await response.Content.ReadAsAsync<OverviewViewModel>();
                    return result;
                }
            }
        }

        public async Task<UserProfileViewModel> GetUserProfileAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = BaseAddress;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);

                using (var response = await httpClient.GetAsync("/api/User/Profile"))
                {
                    var result = await response.Content.ReadAsAsync<UserProfileViewModel>();
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
                    var result = await response.Content.ReadAsAsync<AccountViewModel>();
                    return result;
                }
            }
        }

        #endregion

        //public async Task<ChangePasswordModel> GetUserCurrentPasswordAsync()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = BaseAddress;
        //        httpClient.DefaultRequestHeaders.Authorization =
        //            new AuthenticationHeaderValue("Bearer", AccessToken);

        //        using (var response = await httpClient.GetAsync("/api/User/UserInfo"))
        //        {
        //            response.EnsureSuccessStatusCode();
        //            var result = await response.Content.ReadAsAsync<ChangePasswordModel>();
        //            return result;
        //        }
        //    }
        //}


        public async Task<string> UpdateProfileAsync(UserProfileViewModel model)
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
    }
}