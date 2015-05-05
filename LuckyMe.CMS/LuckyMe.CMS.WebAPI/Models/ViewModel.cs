namespace LuckyMe.CMS.WebAPI.Models
{
    public class CurrentUserInfo
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool HasRegistered { get; set; }
    }

    public class OverviewViewModel
    {
        public string Email { get; set; }
    }

    public class ProfileViewModel
    {
        public string Email { get; set; }
    }

    public class AccountViewModel
    {
        public string Email { get; set; }
    }
}