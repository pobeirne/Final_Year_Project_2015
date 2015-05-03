using System.Collections.Generic;

namespace LuckyMe.CMS.Entity.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public virtual List<UserProviderDTO> UserProviders { get; set; }
    }
}
