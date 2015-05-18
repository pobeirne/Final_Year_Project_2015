using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models.DTO;

namespace LuckyMe.CMS.Data.Repository.Interfaces
{
    public interface IUserProfileRepository
    {
        // Get

        Task<UserProfileDto> GetUserProfileByIdAsync(string userid);

        // Inserts
        Task<bool> InsertUserProfileAsync(UserProfileDto entry);

        // Updates
        Task<bool> UpdateUserProfileAsync(UserProfileDto entry);
        
        // Save
        Task<bool> SaveAllAsync();
    }
}
