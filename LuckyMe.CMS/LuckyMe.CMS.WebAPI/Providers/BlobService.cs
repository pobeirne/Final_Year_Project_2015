using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LuckyMe.CMS.WebAPI.Providers
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
                var account = new CloudStorageAccount(creds, useHttps: true);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UploadFilesToBlob(List<BlobFile> fileList)
        {
            try
            {
                var creds = new StorageCredentials(_accountName, _accountKey);
                var account = new CloudStorageAccount(creds, useHttps: true);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<bool> RemoveFileFromBlob(string filename)
        {
            try
            {
                var creds = new StorageCredentials(_accountName, _accountKey);
                var account = new CloudStorageAccount(creds, useHttps: true);
                var client = account.CreateCloudBlobClient();

                var container = client.GetContainerReference(Basedirectory);

                var blockBlob = container.GetBlockBlobReference(filename);

                await blockBlob.DeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveFilesFromBlob(List<string> fileList)
        {
            try
            {
                var creds = new StorageCredentials(_accountName, _accountKey);
                var account = new CloudStorageAccount(creds, useHttps: true);
                var client = account.CreateCloudBlobClient();

                var container = client.GetContainerReference(Basedirectory);

                foreach (var filename in fileList)
                {
                    var blockBlob = container.GetBlockBlobReference(filename);
                    await blockBlob.DeleteAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<string>> GetFileNamesInContainer(string dir)
        {
            try
            {

                var creds = new StorageCredentials(_accountName, _accountKey);
                var account = new CloudStorageAccount(creds, useHttps: true);
                var client = account.CreateCloudBlobClient();
               
                var container = client.GetContainerReference(Basedirectory);
                var directory = container.GetDirectoryReference(@dir);
                
                if (!await container.ExistsAsync()) return null;

                var blobs = await Task.Run(() => directory.ListBlobs());
                
                var list = blobs.Select(blobItem => blobItem.Uri.AbsolutePath.ToString().Replace("/"+Basedirectory+dir, "")).ToList();
                
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}