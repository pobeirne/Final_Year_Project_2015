using System;

namespace LuckyMe.CMS.WebAPI.Models
{
    public class FileInfoViewModel
    {
        public string FileName { get; set; }
        public string AlbumName { get; set; }
        public string Directory { get; set; }
        public string FileUrl { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
    }
}