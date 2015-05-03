using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyMe.CMS.Entity.DTO;

namespace LuckyMe.CMS.Service.Services.Interfaces
{
    public interface IUserService
    {
        // Get
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        UserDTO GetUserById(string id);

        // Inserts
        bool InsertUserExternalLoginEntry(UserProviderDTO entry);

        // Updates
        bool UpdateUserExternalLoginEntry(UserProviderDTO entry);

        // Deletes
        bool DeleteUserExternalLoginEntry(UserProviderDTO entry);
        
    }
}
