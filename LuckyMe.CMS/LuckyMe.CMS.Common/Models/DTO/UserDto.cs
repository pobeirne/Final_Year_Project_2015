using System.Collections.Generic;

namespace LuckyMe.CMS.Common.Models.DTO
{
    public class UserAccountDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public virtual UserProfileDto UserProfile { get; set; }
        public virtual List<UserClaimDto> UserClaims { get; set; }
    }
}
