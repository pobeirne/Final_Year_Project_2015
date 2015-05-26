using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models;
using LuckyMe.CMS.Common.Models.ViewModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace LuckyMe.CMS.WebAPI.Helpers
{
    public class BlobService
    {
        private readonly string _accountName = ConfigurationManager.AppSettings["Blob_AccountName"];
        private readonly string _accountKey = ConfigurationManager.AppSettings["Blob_AccountKey"];
        private const string Basedirectory = "luckymeblob/";

        public async Task<bool> UploadFileToBlob(BlobFile file)
        {
            try
            {
                var creds = new StorageCredentials(_accountName, _accountKey);
                var account = new CloudStorageAccount(creds, true);
                var client = account.CreateCloudBlobClient();

                var container = client.GetContainerReference(Basedirectory);
                container.CreateIfNotExists();

                var blockBlob = container.GetBlockBlobReference(file.FileName);
                blockBlob.Properties.ContentType = file.FileMime;
                //blockBlob.Metadata.Add("SourceId", file.FileId);
                //blockBlob.Metadata.Add("Source", file.Source);

                var stream = new MemoryStream(file.FileBytes);
                await blockBlob.UploadFromStreamAsync(stream);

                return await blockBlob.ExistsAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UploadFilesToBlob(List<BlobFile> fileList)
        {
            try
            {
                var creds = new StorageCredentials(_accountName, _accountKey);
                var account = new CloudStorageAccount(creds, true);
                var client = account.CreateCloudBlobClient();

                var container = client.GetContainerReference(Basedirectory);
                container.CreateIfNotExists();

                var complete = false;
                foreach (var file in fileList)
                {
                    var blockBlob = container.GetBlockBlobReference(file.FileName);
                    blockBlob.Properties.ContentType = file.FileMime;
                    //blockBlob.Metadata.Add("SourceId", file.FileId);
                    //blockBlob.Metadata.Add("Source", file.Source);

                    var stream = new MemoryStream(file.FileBytes);
                    await blockBlob.UploadFromStreamAsync(stream);
                    complete = await blockBlob.ExistsAsync();
                }
                return complete;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveFileFromBlob(string filename)
        {
            try
            {
                var creds = new StorageCredentials(_accountName, _accountKey);
                var account = new CloudStorageAccount(creds, true);
                var client = account.CreateCloudBlobClient();

                var container = client.GetContainerReference(Basedirectory);

                var blockBlob = container.GetBlockBlobReference(filename);

                await blockBlob.DeleteAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveFilesFromBlob(List<string> fileList)
        {
            try
            {
                var creds = new StorageCredentials(_accountName, _accountKey);
                var account = new CloudStorageAccount(creds, true);
                var client = account.CreateCloudBlobClient();

                var container = client.GetContainerReference(Basedirectory);

                foreach (var filename in fileList)
                {
                    var blockBlob = container.GetBlockBlobReference(filename);
                    await blockBlob.DeleteAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<string>> GetFileNamesInContainer(string dir)
        {
            try
            {
                var creds = new StorageCredentials(_accountName, _accountKey);
                var account = new CloudStorageAccount(creds, true);
                var client = account.CreateCloudBlobClient();

                var container = client.GetContainerReference(Basedirectory);
                var directory = container.GetDirectoryReference(@dir);

                //if (!await container.ExistsAsync()) return null;

                var blobs = directory.ListBlobs();

                var list =
                    blobs.Select(blobItem => blobItem.Uri.AbsolutePath.ToString().Replace("/" + Basedirectory + dir, ""))
                        .ToList();

                return list;
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<FileInfoViewModel>> GetAllAlbumFilesInfoAsync(string userId, string type, string album)
        {
            try
            {
            var creds = new StorageCredentials(_accountName, _accountKey);
            var account = new CloudStorageAccount(creds, true);
            var client = account.CreateCloudBlobClient();

            var container = client.GetContainerReference(Basedirectory);

            //if (!await container.ExistsAsync()) return null;

            var directoryPath = userId + "/" + type + "/" + album;

            var directory = container.GetDirectoryReference(@directoryPath);

            var blobs = directory.ListBlobs();

            var filenames =
                blobs.Select(
                    blobItem => blobItem.Uri.AbsolutePath.ToString().Replace("/" + Basedirectory + directoryPath, ""))
                    .ToList();

            var fileList = new List<FileInfoViewModel>();

            foreach (var item in filenames)
            {

                var blob2 = container.GetBlockBlobReference(directoryPath + item.Replace("%20", " "));

                

               
                blob2.FetchAttributes();


                if (blob2.Properties.LastModified != null)
                    fileList.Add(new FileInfoViewModel
                    {
                        FileName = item,
                        AlbumName = album,
                        Directory = directoryPath,
                        FileUrl = blob2.StorageUri.PrimaryUri.AbsoluteUri,
                        ContentType = blob2.Properties.ContentType,
                        Size = blob2.Properties.Length,
                        LastModified = blob2.Properties.LastModified.Value.DateTime
                    });
            }
            return fileList;

                 }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
        }
    }
}