using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using System.Web.Mvc;
using LuckyMe.CMS.Common.Extensions;
using LuckyMe.CMS.Common.Models;
using LuckyMe.CMS.Common.Models.ViewModels;
using LuckyMe.CMS.Web.ClientHelpers;
using LuckyMe.CMS.Web.Filters;
using LuckyMe.CMS.Web.Models;
using Newtonsoft.Json;

namespace LuckyMe.CMS.Web.Controllers
{
    [UserValidation]
    public class BlobController : Controller
    {
        private UserSession _curruser;
        private readonly LuckyMeClient _client;

        public BlobController()
        {
            _curruser = new UserSession();
            _client = new LuckyMeClient();
        }


        //Get views
        public ActionResult BlobPhotoViewer()
        {
            return View();
        }

        public ActionResult BlobVideoViewer()
        {
            return View();
        }


        public ActionResult BackupToBlobView()
        {
            return View();
        }



        public async Task<ActionResult> GetAllBlobPhotosAsync(int page, int limit, string sort, string filter,
            int start = 0)
        {
            //Authentication 
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;


            ////Get data
            var basequery = await _client.GetAllBlobPhotosAsync();


            //Get data count
            var totalCount = basequery.Count;


            var sorting = new List<SortData>();

            if (sort == null)
            {
                sorting.Add(new SortData {Direction = "ASC", Property = "FileName"});
            }
            else
            {
                var deserzdSort = JsonConvert.DeserializeObject<List<Sorting>>(sort);
                basequery = basequery.OrderBy(deserzdSort[0].Property + " " + deserzdSort[0].Direction).ToList();
            }
            
          
            if (filter != null)
            {
                var deserzdFilter = JsonConvert.DeserializeObject<List<Filtering>>(filter);
                //var count = deserzdFilter.Count;

                for (var i = 0; i < deserzdFilter.Count(); i++)
                {
                    var filterparam = deserzdFilter[i];

                    switch (deserzdFilter[i].Field.ToUpper())
                    {
                        case "FILENAME":
                            basequery =
                                basequery.Where(x => x.FileName.ToLower().StartsWith(filterparam.Value.ToLower()))
                                    .ToList();
                            break;
                    }
                }
            }

            basequery = basequery.Skip(start)
                .Take(limit)
                .ToList();


            //Return data
            return Json(new {success = true, data = basequery, totalCount}, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> GetAllBlobVideosAsync(int page, int limit, string sort, string filter,
            int start = 0)
        {
            //Authentication 
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;


            ////Get data
            var basequery = await _client.GetAllBlobVideosAsync();


            //Get data count
            var totalCount = basequery.Count;


            var sorting = new List<SortData>();

            if (sort == null)
            {
                sorting.Add(new SortData {Direction = "ASC", Property = "FileName"});
            }
            else
            {
                var deserzdSort = JsonConvert.DeserializeObject<List<Sorting>>(sort);
                basequery = basequery.OrderBy(deserzdSort[0].Property + " " + deserzdSort[0].Direction).ToList();
            }
            
            if (filter != null)
            {
                var deserzdFilter = JsonConvert.DeserializeObject<List<Filtering>>(filter);
                //var count = deserzdFilter.Count;

                for (var i = 0; i < deserzdFilter.Count(); i++)
                {
                    var filterparam = deserzdFilter[i];

                    switch (deserzdFilter[i].Field.ToUpper())
                    {
                        case "FILENAME":
                            basequery =
                                basequery.Where(x => x.FileName.ToLower().StartsWith(filterparam.Value.ToLower()))
                                    .ToList();
                            break;
                    }
                }
            }

            basequery = basequery.Skip(start)
                .Take(limit)
                .ToList();


            //Return data
            return Json(new {success = true, data = basequery, totalCount}, JsonRequestBehavior.AllowGet);
        }



        public async Task<ActionResult> GetAllPhotoAlbumsAsync()
        {
            //Authentication 
            _curruser = (UserSession)Session["UserSession"];
            _client.AccessToken = _curruser.Token;

            var albumList = await _client.GetAllBlobFilesAsync();

            var list = albumList.Where(x => x.AlbumType == "Photo").Select(x => x.AlbumName).Distinct().ToList();

            var totalCount = list.Count();

            return Json(new { success = true, data = list, totalCount }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAllVideoAlbumsAsync()
        {
            //Authentication 
            _curruser = (UserSession)Session["UserSession"];
            _client.AccessToken = _curruser.Token;

            var albumList = await _client.GetAllBlobFilesAsync();

            var list = albumList.Where(x => x.AlbumType == "Video").Select(x => x.AlbumName).Distinct().ToList();

            var totalCount = list.Count();

            return Json(new { success = true, data = list, totalCount }, JsonRequestBehavior.AllowGet);
        }




        public async Task<ActionResult> AddPhotosToBlobAsync(List<FacebookPhotoViewModel> model, string album)
        {
            //Authentication 
            _curruser = (UserSession)Session["UserSession"];
            _client.AccessToken = _curruser.Token;

            var fileList = model.Select(file => new BlobFileViewModel()
            {
                Album = album.ToLower(),
                FileName = string.IsNullOrEmpty(file.Name) ? "0" : file.Name,
                FileUrl = file.LargePicture
            }).ToList();

            var addresult = await _client.UploadPhotosToAlbumAsync(fileList);

            return Json(new { success = addresult});
        }

        public async Task<ActionResult> AddVideosToBlobAsync(List<FacebookVideoViewModel> model, string album)
        {
            //Authentication 
            _curruser = (UserSession)Session["UserSession"];
            _client.AccessToken = _curruser.Token;

            var fileList = model.Select(file => new BlobFileViewModel()
            {
                Album = album.ToLower(),
                FileName = file.Name,
                FileUrl = file.Source
            }).ToList();

            var addresult = await _client.UploadVideosToAlbumAsync(fileList);

            return Json(new { success = addresult });
        }


        public async Task<ActionResult> UpdatelBlobVideoAsync(FileInfoViewModel model)
        {
            //Authentication 
            _curruser = (UserSession)Session["UserSession"];
            _client.AccessToken = _curruser.Token;

            var file = new BlobFileViewModel()
            {
                Album = model.AlbumName.ToLower().Replace("/", ""),
                FileName = model.FileName,
                FileUrl = model.FileUrl
            };

            var updateresult = await _client.UploadVideoToAlbumAsync(file);

            return Json(new { success = updateresult });
        }


        public async Task<ActionResult> UpdatelBlobPhotoAsync(FileInfoViewModel model)
        {
            //Authentication 
            _curruser = (UserSession)Session["UserSession"];
            _client.AccessToken = _curruser.Token;

            var file = new BlobFileViewModel()
            {
                Album = model.AlbumName.ToLower().Replace("/", ""),
                FileName = model.FileName,
                FileUrl = model.FileUrl
            };

            var updateresult = await _client.UploadPhotoToAlbumAsync(file);

            return Json(new { success = updateresult });
        }


        public async Task<ActionResult> DeleteBlobVideoAsync(FileInfoViewModel model)
        {
            //Authentication 
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;

            var file = new BlobFileViewModel()
            {
                Album = model.AlbumName.ToLower().Replace("/", ""),
                FileName = model.FileName,
                FileUrl = model.FileUrl
            };

            var result = await _client.DeleteVideoFromAlbumAsync(file);

            return Json(new {success = result});
        }


        public async Task<ActionResult> DeleteBlobPhotoAsync(FileInfoViewModel model)
        {
            //Authentication 
            _curruser = (UserSession) Session["UserSession"];
            _client.AccessToken = _curruser.Token;

            var file = new BlobFileViewModel()
            {
                Album = model.AlbumName.ToLower().Replace("/", ""),
                FileName = model.FileName,
                FileUrl = model.FileUrl
            };

            var result = await _client.RemovePhotoFromAlbumAsync(file);

            return Json(new {success = result});
        }

     
       

    }
}