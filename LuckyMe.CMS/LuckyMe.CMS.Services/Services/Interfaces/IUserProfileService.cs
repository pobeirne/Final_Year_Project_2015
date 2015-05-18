using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models.DTO;

namespace LuckyMe.CMS.Service.Services.Interfaces
{
    public interface IUserProfileService
    {
        // Get
        Task<UserProfileDto> GetUserProfileByIdAsync(string userid);

        // Inserts
        Task<bool> InsertUserProfileAsync(UserProfileDto entry);

        // Updates
        Task<bool> UpdateUserProfileAsync(UserProfileDto entry);
    }
}
