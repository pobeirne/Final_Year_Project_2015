using System;
using System.Threading.Tasks;
using LuckyMe.CMS.Web.Client;

namespace LuckyMe.CMS.Web.Models
{
    public class UserSession
    {
        public string Token { get; set; }
        public Uri BaseAddress { get; set; }
    }
}