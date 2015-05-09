using System;
using System.ComponentModel.DataAnnotations;
using LuckyMe.CMS.Common.Attributes;

namespace LuckyMe.CMS.Common.Models.ViewModels.fb
{
    public class FacebookAlbumViewModel
    {
        [Microsoft.Build.Framework.Required]
        [FacebookMapping("id")]
        public string Id { get; set; }

        [Microsoft.Build.Framework.Required]
        [Display(Name = "Album Name")]
        [FacebookMapping("name")]
        public string Name { get; set; }

        [Display(Name = "Photo Count")]
        [FacebookMapping("count")]
        public long Count { get; set; }

        [Display(Name = "Link")]
        [FacebookMapping("link")]
        public string Link { get; set; }

        [FacebookMapping("url", Parent = "picture")]
        public string ImageUrl { get; set; }

        [Display(Name = "LastUpdated")]
        [FacebookMapping("updated_time")]
        public DateTime LastUpdated { get; set; }
    }
}