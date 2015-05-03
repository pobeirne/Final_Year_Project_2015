namespace LuckyMe.CMS.Web.Models
{
    public class UserInfoViewModel
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
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Fullname { get; set; }
        //public string Gender { get; set; }
        //public string ImageUrl { get; set; }
        //public string LinkUrl { get; set; }
        //public string Location { get; set; }
        //public string ProfileType { get; set; }
    }

    public class AccountViewModel
    {
        public string Email { get; set; }
    }
}