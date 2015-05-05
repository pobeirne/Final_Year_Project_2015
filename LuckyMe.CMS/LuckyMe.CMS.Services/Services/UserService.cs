using System.Collections.Generic;
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
        
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return await _respository.GetAllUsersAsync();
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            return await _respository.GetUserByIdAsync(id);
        }

        public async Task<bool> InsertUserClaimAsync(UserClaimDTO claim)
        {
            var user = await GetUserByIdAsync(claim.UserId);
            if (user == null) return false;

            if (user.UserClaims.Exists(x => x.ClaimType == claim.ClaimType))
            {
                await _respository.UpdateUserClaimAsync(claim);
                return await _respository.SaveAllAsync();
            }
            if (await _respository.InsertUserClaimAsync(claim))
            {
                return await _respository.SaveAllAsync();
            }
            return false;
        }

        public async Task<bool> UpdateUserClaimAsync(UserClaimDTO claim)
        {
            if (!await _respository.UpdateUserClaimAsync(claim)) return false;
            return await _respository.SaveAllAsync();
        }

        public async Task<bool> DeleteUserClaimAsync(UserClaimDTO claim)
        {
            if (!await _respository.DeleteUserClaimAsync(claim)) return false;
            return await _respository.SaveAllAsync();
        }

        public async Task<bool> DeleteUserAsync(UserDTO user)
        {
            if (!await _respository.DeleteUserAsync(user)) return false;
            return await _respository.SaveAllAsync();
        }


    }
}
