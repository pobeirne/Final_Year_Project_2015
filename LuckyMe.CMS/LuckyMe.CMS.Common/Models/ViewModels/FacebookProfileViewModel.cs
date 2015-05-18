using System.ComponentModel.DataAnnotations;
using LuckyMe.CMS.Common.Attributes;

namespace LuckyMe.CMS.Common.Models.ViewModels
{
    public class FacebookProfileViewModel
    {
        [Required]
        [Display(Name = "Id")]
        [FacebookMapping("id")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [FacebookMapping("first_name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [FacebookMapping("last_name")]
        public string LastName { get; set; }

        [FacebookMapping("name")]
        public string Fullname { get; set; }

        public string ImageUrl { get; set; }

        [FacebookMapping("link")]
        public string LinkUrl { get; set; }

        [FacebookMapping("locale")]
        public string Locale { get; set; }

        [FacebookMapping("email")]
        public string Email { get; set; }
 
        [FacebookMapping("gender")]
        public string Gender { get; set; }

        
    }
}