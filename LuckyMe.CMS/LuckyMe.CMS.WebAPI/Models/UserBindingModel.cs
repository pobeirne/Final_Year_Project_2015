using System.ComponentModel.DataAnnotations;

namespace LuckyMe.CMS.WebAPI.Models
{
    public class UserProviderBindingModel
    {
        [Required]
        [Display(Name = "External provider name")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "External provider access token")]
        public string ProviderKey { get; set; }

    }
}