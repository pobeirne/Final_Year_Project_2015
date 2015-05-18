using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LuckyMe.CMS.Common.Models;
using LuckyMe.CMS.Common.Models.ViewModels;
using LuckyMe.CMS.Web.ClientHelpers;
using LuckyMe.CMS.Web.Filters;
using LuckyMe.CMS.Web.Models;
using Newtonsoft.Json;

namespace LuckyMe.CMS.Web.Controllers
{
    [UserValidation]
    public class DashboardController : Controller
    {
        private UserSession _curruser;
        private readonly LuckyMeClient _client;
        
        public DashboardController()
        {
            _curruser = new UserSession();
            _client = new LuckyMeClient();
        }

        public async Task<ActionResult> UserOverview()
        {
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;

            //Facbook
            //////////////////////////////////////////////////////////////////////////////////////////////
            //working
            //var testGetFaceBookProfile = await _client.GetFacebookProfileAsync();

            //working
            //var albums = await _client.GetAllFacebookAlbumsAsync();

            //Working
            //var album = await _client.GetFacebookAlbumAsync("381709418695884");

            //Working
            //var albumphotos = await _client.GetAlbumPhotosAsync("381709418695884");
            
            //Working
            //var photo = await _client.GetAlbumPhotoAsync("381709418695884", "381709425362550");

            //Working
            //var videos = await _client.GetAllVideosAsync();

            //Working
            //var video = await _client.GetVideoAsync("381730758693750");


            //AzureBlob
            /////////////////////////////////////////////////////////////////////////////////////////////
            //Working
            //var uploadPhotoFile = await _client.UploadPhotoToAlbumAsync(new BlobFileViewModel()
            //{
            //    FileName = "PaulTest123.jpg",
            //    FileUrl = Imageurl,
            //    Album = "Testing12345678"

            //});

            //Working
            //var photoList = new List<BlobFileViewModel>()
            //{
            //    new BlobFileViewModel()
            //    {
            //        FileName = "PaulTestphotolist1.jpg",
            //        FileUrl = Imageurl,
            //        Album = "Testing12345678"
            //    },
            //    new BlobFileViewModel()
            //    {
            //        FileName = "PaulTestphotolist2.jpg",
            //        FileUrl = Imageurl,
            //        Album = "Testing12345678"
            //    }
            //};
            //var result = await _client.UploadPhotosToAlbumAsync(photoList);


            //Working
            //var uploadVideoFile = await _client.UploadVideoToAlbumAsync(new BlobFileViewModel()
            //{
                //FileName = "PaulTest123.mp4",
                //FileUrl = Videourl,
                //Album = "vTesting12345678"

            //});

            //Working
            //var videoList = new List<BlobFileViewModel>()
            //{
            //    new BlobFileViewModel()
            //    {
            //    FileName = "PaulTest1.mp4",
            //    FileUrl = Videourl,
            //    Album = "vTestinglist8"
            //    },
            //    new BlobFileViewModel()
            //    {
            //    FileName = "PaulTest2.mp4",
            //    FileUrl = Videourl,
            //    Album = "vTestinglist8"
            //    }
            //};
            //var result = await _client.UploadVideosToAlbumAsync(videoList);


            //Working
            //var removePhotoFile = await _client.RemovePhotoFromAlbumAsync(new BlobFileViewModel()
            //{
            //    FileName = "PaulTestphotolist2.jpg",
            //    FileUrl = Imageurl,
            //    Album = "Testing12345678"

            //});


            //var files = await _client.GetAllBlobFilesAsync();

            var overview = await _client.GetUserOverviewAsync();
            return View(overview);
        }

        //public async Task<ActionResult> UserProfile()
        //{
        //    _curruser = (UserSession) Session["UserSession"];
        //    _client.AccessToken = _curruser.Token;
        //    UserProfileViewModel profile = await _client.GetUserProfileAsync();
        //    return View(profile);
        //}

        //public async Task<ActionResult> UserAccount()
        //{
        //    _curruser = (UserSession) Session["UserSession"];
        //    _client.AccessToken = _curruser.Token;
        //    AccountViewModel account = await _client.GetUserAccountAsync();
        //    return View(account);
        //}


        public ActionResult ShowFaceBookVideoGrid()
        {
            return View();
        }
        

        //GetAllFaceBookVideosAsync
        public async Task<ActionResult> GetAllFaceBookVideosAsync(int page, int limit, string sort, string filter, int start = 0)
        {
            //Authentication 
            _curruser = (UserSession)Session["UserSession"];
            _client.AccessToken = _curruser.Token;

            //Get data
            var model = await _client.GetAllVideosAsync();

            //Get data count
            var totalCount = model.Count;

            var sorts = JsonConvert.DeserializeObject<List<Sorting>>(sort);
            var filters = JsonConvert.DeserializeObject<List<Filtering>>(sort);

            var dir = sorts[0].Direction;
            var prop = sorts[0].Property;
            
            //Some validation needed

            //Return data
            return Json(new { success = true, data = model.Skip(start).Take(limit), totalCount }, JsonRequestBehavior.AllowGet);
        }

    }
}