using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models.DTO;
using LuckyMe.CMS.Data.Repository.Interfaces;
using LuckyMe.CMS.Service.Services.Interfaces;

namespace LuckyMe.CMS.Service.Services
{
    public class UserClaimService : IUserClaimService
    {
        private readonly IUserClaimRepository _claimRepository;
        private readonly IUserRepository _userRespository;

        public UserClaimService(
            IUserRepository userRespository,
            IUserClaimRepository claimRepository)
        {
            _userRespository = userRespository;
            _claimRepository = claimRepository;
        }

        public async Task<IEnumerable<UserClaimDto>> GetAllUserClaimsAsync(string userid)
        {
            try
            {
                var user = await _userRespository.GetUserByIdAsync(userid);
                if (user == null) return null;
                return await _claimRepository.GetAllUserClaimsAsync(user.Id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> InsertUserClaimAsync(UserClaimDto entry)
        {
            try
            {
                var user = await _userRespository.GetUserByIdAsync(entry.UserId);
                if (user == null) return false;
                return await _claimRepository.InsertUserClaimAsync(entry);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserClaimAsync(UserClaimDto entry)
        {
            try
            {
                var user = await _userRespository.GetUserByIdAsync(entry.UserId);
                if (user == null) return false;
                return await _claimRepository.UpdateUserClaimAsync(entry);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserClaimAsync(UserClaimDto entry)
        {
            try
            {
                var user = await _userRespository.GetUserByIdAsync(entry.UserId);
                if (user == null) return false;
                return await _claimRepository.DeleteUserClaimAsync(entry);
            }
            catch
            {
                return false;
            }
        }
    }
}
