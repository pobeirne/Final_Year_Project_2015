using System.ComponentModel.DataAnnotations;

namespace LuckyMe.CMS.Common.Models.ViewModels
{
    public class BlobFileViewModel
    {
        [Required]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required]
        [Display(Name = "File URL")]
        public string FileUrl { get; set; }

        [Required]
        [Display(Name = "Album Name")]
        public string Album { get; set; }
    }
}
