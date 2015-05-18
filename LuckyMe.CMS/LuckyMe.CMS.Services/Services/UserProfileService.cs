using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models.DTO;
using LuckyMe.CMS.Data.Repository.Interfaces;
using LuckyMe.CMS.Service.Services.Interfaces;

namespace LuckyMe.CMS.Service.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _profileRepository;
        private readonly IUserRepository _userRespository;

        public UserProfileService(
            IUserRepository userRespository,
            IUserProfileRepository profileRepository)
        {
            _userRespository = userRespository;
            _profileRepository = profileRepository;
        }

        public async Task<UserProfileDto> GetUserProfileByIdAsync(string userid)
        {
            try
            {
                var user = await _userRespository.GetUserByIdAsync(userid);
                if (user == null) return null;
                return await _profileRepository.GetUserProfileByIdAsync(user.Id);
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<bool> InsertUserProfileAsync(UserProfileDto entry)
        {
            try
            {
                var user = await _userRespository.GetUserByIdAsync(entry.UserId);
                if (user == null) return false;
                return await _profileRepository.InsertUserProfileAsync(entry);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserProfileAsync(UserProfileDto entry)
        {
            try
            {
                var user = await _userRespository.GetUserByIdAsync(entry.UserId);
                if (user == null) return false;
                return await _profileRepository.UpdateUserProfileAsync(entry);
            }
            catch
            {
                return false;
            }
        }
    }
}
