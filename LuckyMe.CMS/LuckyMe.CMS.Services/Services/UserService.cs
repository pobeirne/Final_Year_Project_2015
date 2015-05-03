using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuckyMe.CMS.Data.Repository.Interfaces;
using LuckyMe.CMS.Entity.DTO;
using LuckyMe.CMS.Service.Services.Interfaces;

namespace LuckyMe.CMS.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _respository;

        public UserService(IUserRepository respository)
        {
            _respository = respository;
        }

        //public IEnumerable<UserDTO> GetAllUsers()
        //{
        //    return _respository.GetAllUsers();
        //}

        public UserDTO GetUserById(string id)
        {
            return _respository.GetUserById(id);
        }

        public bool InsertUserExternalLoginEntry(UserProviderDTO entry)
        {
            var user = GetUserById(entry.UserId);
            if (user != null)
            {
                if (!user.UserProviders.Any(x => x.LoginProvider == entry.LoginProvider
                                                 && x.ProviderKey == entry.ProviderKey))
                {
                    if (_respository.InsertUserExternalLoginEntry(entry))
                    {
                        if (_respository.SaveAll())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool UpdateUserExternalLoginEntry(UserProviderDTO entry)
        {
            if (_respository.UpdateUserExternalLoginEntry(entry))
            {
                if (_respository.SaveAll())
                {
                    return true;
                }
            }

            return false;
        }

        public bool DeleteUserExternalLoginEntry(UserProviderDTO entry)
        {
            if (_respository.DeleteUserExternalLoginEntry(entry))
            {
                if (_respository.SaveAll())
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return await _respository.GetAllUsersAsync();
        }
    }
}
