using System.Linq;
using System.Threading.Tasks;
using LuckyMe.CMS.Entity.DTO;

namespace LuckyMe.CMS.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        // Get
        Task<IQueryable<UserDTO>> GetAllUsersAsync();

        Task<UserDTO> GetUserByIdAsync(string id);

        // Inserts
        Task<bool> InsertUserClaimAsync(UserClaimDTO entry);

        // Updates
        Task<bool> UpdateUserClaimAsync(UserClaimDTO entry);

        // Deletes
        Task<bool> DeleteUserClaimAsync(UserClaimDTO entry);

        Task<bool> DeleteUserAsync(UserDTO user);

        // Save
        Task<bool> SaveAllAsync();
    }
}
