using System.Collections.Generic;

namespace LuckyMe.CMS.Common.Models.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public virtual List<UserClaimDto> UserClaims { get; set; }
        public virtual List<UserProfileDto> UserProfiles { get; set; }
    }
}
