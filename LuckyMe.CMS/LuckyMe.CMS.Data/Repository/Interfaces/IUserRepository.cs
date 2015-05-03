using System.Linq;
using System.Threading.Tasks;
using LuckyMe.CMS.Entity.DTO;

namespace LuckyMe.CMS.Data.Repository.Interfaces
{
    public interface IUserRepository
    {

        // Get
        Task<IQueryable<UserDTO>> GetAllUsersAsync();
        UserDTO GetUserById(string id);

        // Inserts
        bool InsertUserExternalLoginEntry(UserProviderDTO entry);

        // Updates
        bool UpdateUserExternalLoginEntry(UserProviderDTO entry);

        // Deletes
        bool DeleteUserExternalLoginEntry(UserProviderDTO entry);
  
        // General
        bool SaveAll();

    }
}
