using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models.DTO;

namespace LuckyMe.CMS.Service.Services.Interfaces
{
    public interface IUserService
    {

        // Get
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<UserDto> GetUserByIdAsync(string id);

        // Inserts
        Task<bool> InsertUserClaimAsync(UserClaimDto entry);

        // Updates
        Task<bool> UpdateUserClaimAsync(UserClaimDto entry);

        // Deletes
        Task<bool> DeleteUserClaimAsync(UserClaimDto entry);

        Task<bool> DeleteUserAsync(UserDto user);




    }
}
