using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyMe.CMS.Entity.DTO;

namespace LuckyMe.CMS.Service.Services.Interfaces
{
    public interface IUserService
    {

        // Get
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        Task<UserDTO> GetUserByIdAsync(string id);

        // Inserts
        Task<bool> InsertUserClaimAsync(UserClaimDTO entry);

        // Updates
        Task<bool> UpdateUserClaimAsync(UserClaimDTO entry);

        // Deletes
        Task<bool> DeleteUserClaimAsync(UserClaimDTO entry);

        Task<bool> DeleteUserAsync(UserDTO user);




    }
}
