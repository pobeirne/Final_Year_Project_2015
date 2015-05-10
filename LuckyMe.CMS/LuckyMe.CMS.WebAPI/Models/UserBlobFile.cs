using System.Collections.Generic;

namespace LuckyMe.CMS.WebAPI.Models
{
    public class UserBlobFile
    {
        public string AlbumName { get; set; }

        public string AlbumType { get; set; }

        public List<FileInfoViewModel> AlbumFiles { get; set; }
    }
}