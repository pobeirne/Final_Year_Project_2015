using System.ComponentModel.DataAnnotations;

namespace LuckyMe.CMS.WebAPI.Models
{
    public class UserClaimsViewModel
    {
        [Required]
        [Display(Name = "External provider name")]
        public string ClaimType { get; set; }

        [Required]
        [Display(Name = "External provider access token")]
        public string ClaimValue { get; set; }
    }
}