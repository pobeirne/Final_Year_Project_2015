using System;
using System.ComponentModel.DataAnnotations;
using LuckyMe.CMS.Common.Attributes;

namespace LuckyMe.CMS.Common.Models.ViewModels.fb
{
    public class FacebookVideoViewModel
    {
        [Required]
        [FacebookMapping("id")]
        public string Id { get; set; }

        [FacebookMapping("name")]
        public string Name { get; set; }

        [FacebookMapping("description")]
        public string Description { get; set; }

        [FacebookMapping("picture")]
        public string Picture { get; set; }

        [FacebookMapping("embed_html")]
        public string EmbedHtml { get; set; }

        [FacebookMapping("source")]
        public string Source { get; set; }

        [FacebookMapping("created_time")]
        public DateTime CreateDateTime { get; set; }
    }
}