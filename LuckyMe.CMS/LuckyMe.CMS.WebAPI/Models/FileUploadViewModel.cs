using System.ComponentModel.DataAnnotations;

namespace LuckyMe.CMS.WebAPI.Models
{
    public class FileUploadViewModel
    {
        [Required]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "File Url")]
        public string FileUrl { get; set; }

        [Required]
        [Display(Name = "File Mime")]
        public string FileMime { get; set; }

        [Required]
        [Display(Name = "Album Name")]
        public string AlbumName { get; set; }
        
    }
}