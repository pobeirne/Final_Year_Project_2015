using System;


namespace LuckyMe.CMS.Web.Models
{
    public class UserSession
    {
        public string Token { get; set; }
        public Uri BaseAddress { get; set; }
    }
}