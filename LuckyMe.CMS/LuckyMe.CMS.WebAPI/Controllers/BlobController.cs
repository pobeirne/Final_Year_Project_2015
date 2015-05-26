using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using LuckyMe.CMS.Common.Models;
using LuckyMe.CMS.Common.Models.ViewModels;
using LuckyMe.CMS.Service.Services.Interfaces;
using LuckyMe.CMS.WebAPI.Helpers;
using Microsoft.AspNet.Identity;

namespace LuckyMe.CMS.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Blob")]
    public class BlobController : ApiController
    {
        private readonly IUserService _userservice;
        private readonly BlobService _blobService;

        private readonly string _imageMime;
        private readonly string _videoMime;

        public BlobController()
        {
        }

        public BlobController(IUserService userservice)
        {
            _userservice = userservice;
            _blobService = new BlobService();
            _imageMime = "image/jpg";
            _videoMime = "video/mp4";
        }

        #region Get

        [Route("GetAllBlobPhotos")]
        public async Task<IHttpActionResult> GetAllBlobPhotos()
        {
           
            var userid = FormatUserId(User.Identity.GetUserId());
            if (userid == null) return BadRequest();

            //Get All photo albums
            var albumlist1 = await _blobService.GetFileNamesInContainer(userid + "/photos/");

            var fileList = new List<FileInfoViewModel>();
            foreach (var album in albumlist1)
            {
                fileList.AddRange(_blobService.GetAllAlbumFilesInfoAsync(userid, "photos", album).Result);
            }
            return Ok(fileList);
        }


        [Route("GetAllBlobVideos")]
        public async Task<IHttpActionResult> GetAllBlobVideos()
        {
           
                var userid = FormatUserId(User.Identity.GetUserId());
                if (userid == null) return NotFound();

                //Get All video albums
                var albumlist1 = await _blobService.GetFileNamesInContainer(userid + "/videos/");

                var fileList = new List<FileInfoViewModel>();
                foreach (var album in albumlist1)
                {
                    fileList.AddRange(_blobService.GetAllAlbumFilesInfoAsync(userid, "videos", album).Result);
                }
           
                return Ok(fileList);
        }


        [Route("GetAllBlobFiles")]
        public async Task<IHttpActionResult> GetAllBlobFiles()
        {
            var userid = FormatUserId(User.Identity.GetUserId());
            if (userid == null) return NotFound();

            //Get All photo albums
            var albumlist1 = await _blobService.GetFileNamesInContainer(userid + "/photos/");

            var photoAlbumList = albumlist1.Select(album => new UserBlobFile()
            {
                AlbumName = album,
                AlbumType = "Photo"
            }).ToList();


            //Get All video albums
            var albumlist2 = await _blobService.GetFileNamesInContainer(userid + "/videos/");

            var videoAlbumList = albumlist2.Select(album => new UserBlobFile()
            {
                AlbumName = album,
                AlbumType = "Video"
               
            }).ToList();

            var albumlist = photoAlbumList.Concat(videoAlbumList);

            return Ok(albumlist);
        }

        #endregion

        #region Upload  

        [Route("UploadPhotoToAlbum")]
        public async Task<IHttpActionResult> UploadPhotoToAlbumAsync(BlobFileViewModel photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());
            var directory = CreateDirectoryPath(user.Id, "/photos/", photo.Album.ToLower());

            var result = await _blobService.UploadFileToBlob(new BlobFile
            {
                FileName = directory + photo.FileName,
                FileMime = _imageMime,
                FileBytes = GetFileData(photo.FileUrl)
            });

            //var list = await _blobService.GetFileNamesInContainer(directory);

            if (result)
            {
                return Ok();
            }
            return BadRequest("");
        }

        [Route("UploadPhotosToAlbum")]
        public async Task<IHttpActionResult> UploadPhotosToAlbumAsync(IEnumerable<BlobFileViewModel> photoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var fileList = new List<BlobFile>();

            await Task.Run(() =>
            {
                fileList = (from photo in photoList
                            let directory = CreateDirectoryPath(user.Id, "/photos/", photo.Album.ToLower())
                    select new BlobFile()
                    {
                        FileName = directory + photo.FileName,
                        FileMime = _imageMime,
                        FileBytes = GetFileData(photo.FileUrl)
                    }).ToList();
            });

            var result = await _blobService.UploadFilesToBlob(fileList);

            if (result)
            {
                return Ok();
            }
            return BadRequest("");
        }

        [Route("UploadVideoToAlbum")]
        public async Task<IHttpActionResult> UploadVideoToAlbumAsync(BlobFileViewModel video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());
            var directory = CreateDirectoryPath(user.Id, "/videos/", video.Album);

            var result = await _blobService.UploadFileToBlob(new BlobFile
            {
                FileName = directory + video.FileName,
                FileMime = _videoMime,
                FileBytes = GetFileData(video.FileUrl)
            });

            if (result)
            {
                return Ok();
            }
            return BadRequest("");
        }

        [Route("UploadVideosToAlbum")]
        public async Task<IHttpActionResult> UploadVideosToAlbumAsync(IEnumerable<BlobFileViewModel> videoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var fileList = new List<BlobFile>();

            await Task.Run(() =>
            {
                fileList = (from video in videoList
                    let directory = CreateDirectoryPath(user.Id, "/videos/", video.Album)
                    select new BlobFile()
                    {
                        FileName = directory + video.FileName,
                        FileMime = _videoMime,
                        FileBytes = GetFileData(video.FileUrl)
                    }).ToList();
            });

            if (await _blobService.UploadFilesToBlob(fileList))
            {
                return Ok();
            }
            return BadRequest("");
        }

        #endregion

        #region Remove

        [Route("RemovePhotoFromAlbum")]
        public async Task<IHttpActionResult> RemovePhotoFromAlbumAsync(BlobFileViewModel photo)
        {
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var directory = CreateDirectoryPath(user.Id, "/photos/", photo.Album);

            var result = await _blobService.RemoveFileFromBlob(directory + photo.FileName);

            if (result)
            {
                return Ok();
            }
            return BadRequest("");
        }

        [Route("RemovePhotosFromAlbum")]
        public async Task<IHttpActionResult> RemovePhotosFromAlbumAsync(List<BlobFileViewModel> photoList)
        {
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());


            var fileList = new List<string>();

            await Task.Run(() =>
            {
                fileList = (from photo in photoList
                    let directory = CreateDirectoryPath(user.Id, "/photos/", photo.Album)
                    select directory + photo.FileName).ToList();
            });

            var result = await _blobService.RemoveFilesFromBlob(fileList);

            if (result)
            {
                return Ok();
            }
            return BadRequest("");
        }

        [Route("RemoveVideoFromAlbum")]
        public async Task<IHttpActionResult> RemoveVideoFromBlobAsync(BlobFileViewModel video)
        {
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());

            var directory = CreateDirectoryPath(user.Id, "/videos/", video.Album);

            var result = await _blobService.RemoveFileFromBlob(directory + video.FileName);

            if (result)
            {
                return Ok();
            }
            return BadRequest("");
        }

        [Route("RemoveVideosFromAlbum")]
        public async Task<IHttpActionResult> RemoveVideosFromBlobAsync(List<BlobFileViewModel> videoList)
        {
            var user = await _userservice.GetUserByIdAsync(User.Identity.GetUserId());


            var fileList = new List<string>();

            await Task.Run(() =>
            {
                fileList = (from video in videoList
                    let directory = CreateDirectoryPath(user.Id, "/videos/", video.Album)
                    select directory + video.FileName).ToList();
            });

            var result = await _blobService.RemoveFilesFromBlob(fileList);

            if (result)
            {
                return Ok();
            }
            return BadRequest("");
        }

        #endregion

        #region Private

        private static byte[] GetFileData(string url)
        {
            using (var client = new WebClient())
            {
                var data = client.DownloadData(url);
                return data;
            }
        }

        private static string CreateDirectoryPath(string id, string type, string album)
        {
            return id.Replace("-", "") + type + album + "/";
        }

        private static string FormatUserId(string id)
        {
            return id.Replace("-", "");
        }

        #endregion
    }
}
