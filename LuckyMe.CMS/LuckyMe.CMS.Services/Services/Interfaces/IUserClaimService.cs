using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models.DTO;

namespace LuckyMe.CMS.Service.Services.Interfaces
{
    public interface IUserClaimService
    {
        // Get
        Task<IEnumerable<UserClaimDto>> GetAllUserClaimsAsync(string userid);

        // Inserts
        Task<bool> InsertUserClaimAsync(UserClaimDto entry);

        // Updates
        Task<bool> UpdateUserClaimAsync(UserClaimDto entry);

        // Deletes
        Task<bool> DeleteUserClaimAsync(UserClaimDto entry);

    }
}
