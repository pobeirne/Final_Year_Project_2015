using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models.DTO;
using LuckyMe.CMS.Data.Repository.Interfaces;

namespace LuckyMe.CMS.Data.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private static LuckyMeCMSEntityContext _context;

        public UserProfileRepository(LuckyMeCMSEntityContext context)
        {
            _context = context;
        }

        public async Task<UserProfileDto> GetUserProfileByIdAsync(string userid)
        {
            try
            {
                var profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userid);
                if (profile == null)
                {
                    return null;
                }

                return new UserProfileDto()
                {
                    //Id = profile.Id,
                    UserId = profile.UserId,
                    UserName = profile.Name,
                    FirstName = profile.First_Name,
                    LastName = profile.Last_Name,
                    ImageUrl = profile.ImageUrl,
                    ProfileType = profile.ProfileType
                };
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
                var profile = _context.UserProfiles.FirstOrDefault(x => x.UserId == entry.UserId);
                if (profile == null) return false;

                profile.First_Name = entry.FirstName;
                profile.Last_Name = entry.LastName;
                profile.Name = entry.FirstName + " " + entry.LastName;
                profile.ImageUrl = entry.ImageUrl;
                profile.ProfileType = entry.ProfileType;

                _context.UserProfiles.Attach(profile);

                return await SaveAllAsync();
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
                var profile = _context.UserProfiles.FirstOrDefault(x => x.UserId == entry.UserId);
                if (profile == null) return false;

                profile.First_Name = entry.FirstName;
                profile.Last_Name = entry.LastName;
                profile.Name = entry.FirstName + " " + entry.LastName;
                profile.ImageUrl = entry.ImageUrl;
                profile.ProfileType = entry.ProfileType;

                _context.UserProfiles.Attach(profile);

                return await SaveAllAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
