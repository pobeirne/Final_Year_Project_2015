using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models.DTO;
using LuckyMe.CMS.Data.Repository.Interfaces;

namespace LuckyMe.CMS.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private static LuckyMeCMSEntityContext _context;

        public UserRepository(LuckyMeCMSEntityContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.AspNetUsers.ToListAsync();

            var userList = users.Select(user => new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserClaims =
                    _context.AspNetUserClaims.Where(x => x.UserId == user.Id).Select(claim => new UserClaimDto()
                    {
                        Id = claim.Id,
                        ClaimType = claim.ClaimType,
                        ClaimValue = claim.ClaimValue,
                        UserId = claim.UserId
                    }).ToList()
            });

            return userList.AsQueryable();
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var query = await _context.AspNetUsers.FindAsync(id);
            if (query == null) return null;
            var user = new UserDto()
            {
                Id = query.Id,
                Email = query.Email,
                UserName = query.UserName,
                UserClaims =
                    _context.AspNetUserClaims.Where(x => x.UserId == query.Id).Select(claim => new UserClaimDto()
                    {
                        Id = claim.Id,
                        ClaimType = claim.ClaimType,
                        ClaimValue = claim.ClaimValue,
                        UserId = claim.UserId
                    }).ToList()
            };

            return user;
        }

        public async Task<bool> InsertUserClaimAsync(UserClaimDto entry)
        {
            try
            {
                var record = _context.AspNetUserClaims.Create();
                record.UserId = entry.UserId;
                record.ClaimType = entry.ClaimType;
                record.ClaimValue = entry.ClaimValue;
                _context.AspNetUserClaims.Add(record);
                return await SaveAllAsync();
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
                var claim = await _context.AspNetUserClaims.FirstOrDefaultAsync(x =>
                    x.UserId == entry.UserId && x.ClaimType == entry.ClaimType);

                if (claim != null)
                {
                    claim.ClaimValue = entry.ClaimValue;
                    _context.AspNetUserClaims.Attach(claim);
                    return await SaveAllAsync();
                }
                return false;
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
                var claim = await _context.AspNetUserClaims.FirstOrDefaultAsync(
                    x =>
                        x.UserId == entry.UserId && x.ClaimType == entry.ClaimType);

                if (claim != null)
                {
                    _context.AspNetUserClaims.Remove(claim);
                    return await SaveAllAsync();
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(UserDto entry)
        {
            try
            {
                var user = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.Id == entry.Id);

                if (user != null)
                {
                    _context.AspNetUsers.Remove(user);
                    return await SaveAllAsync();
                }
                return false;
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


