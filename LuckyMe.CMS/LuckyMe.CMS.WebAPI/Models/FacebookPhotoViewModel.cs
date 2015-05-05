using System;
using System.ComponentModel.DataAnnotations;
using LuckyMe.CMS.WebAPI.Attributes;

namespace LuckyMe.CMS.WebAPI.Models
{
    public class FacebookPhotoViewModel
    {
        [Required]
        [FacebookMapping("id")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        [FacebookMapping("name")]
        public string Name { get; set; }

        [FacebookMapping("picture")]
        public string SmallPicture { get; set; }

        [FacebookMapping("source")]
        public string LargePicture { get; set; }

        [Display(Name = "CreateDateTime")]
        [FacebookMapping("created_time")]
        public DateTime CreateDateTime { get; set; }
    }
}