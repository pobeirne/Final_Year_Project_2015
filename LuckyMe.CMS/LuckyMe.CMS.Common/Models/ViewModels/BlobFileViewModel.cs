using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
