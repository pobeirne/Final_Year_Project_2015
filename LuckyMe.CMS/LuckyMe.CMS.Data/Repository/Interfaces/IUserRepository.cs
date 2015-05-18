using System.Linq;
using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models.DTO;

namespace LuckyMe.CMS.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        // Get
        Task<IQueryable<UserAccountDto>> GetAllUsersAsync();

        Task<UserAccountDto> GetUserByIdAsync(string id);

        //// Inserts
        //Task<bool> InsertUserClaimAsync(UserClaimDto entry);

        //// Updates
        //Task<bool> UpdateUserClaimAsync(UserClaimDto entry);

        //// Deletes
        //Task<bool> DeleteUserClaimAsync(UserClaimDto entry);

        Task<bool> DeleteUserAsync(UserAccountDto user);

        // Save
        Task<bool> SaveAllAsync();
    }
}
