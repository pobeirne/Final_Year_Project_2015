using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using LuckyMe.CMS.Common.Models.ViewModels;
using LuckyMe.CMS.Web.Clients;
using LuckyMe.CMS.Web.Filters;
using LuckyMe.CMS.Web.Models;

namespace LuckyMe.CMS.Web.Controllers
{
    [UserValidation]
    public class DashboardController : Controller
    {
        private UserSession _curruser;
        private readonly LuckyMeClient _client;

        private const string Imageurl =
            "https://scontent.xx.fbcdn.net/hphotos-xpa1/t31.0-8/s720x720/11194399_381709425362550_2413106543090888216_o.jpg";

        private const string Videourl = "https://fbcdn-video-k-a.akamaihd.net/hvideo-ak-xpt1/v/t43.1792-2/11152976_381730802027079_2061118313_n.mp4?efg=eyJybHIiOjE1MDAsInJsYSI6MTAyNCwidmVuY29kZV90YWciOiJsZWdhY3lfaGQifQ%3D%3D&rl=1500&vabr=490&oh=77f53363cbd3497cc6289a0e6bda434b&oe=55507CA1&__gda__=1431337647_804e7336dd24458e0a2920d6b871726f";

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

            var overview = await _client.GetUserOverviewAsync();
            return View(overview);
        }

        public async Task<ActionResult> UserProfile()
        {
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            ProfileViewModel profile = await _client.GetUserProfileAsync();
            return View(profile);
        }

        public async Task<ActionResult> UserAccount()
        {
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;
            AccountViewModel account = await _client.GetUserAccountAsync();
            return View(account);
        }
    }
}