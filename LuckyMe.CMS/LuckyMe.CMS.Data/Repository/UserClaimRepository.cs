using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LuckyMe.CMS.Common.Models.DTO;
using LuckyMe.CMS.Data.Repository.Interfaces;

namespace LuckyMe.CMS.Data.Repository
{
    public class UserClaimRepository : IUserClaimRepository
    {
        private static LuckyMeCMSEntityContext _context;

        public UserClaimRepository(LuckyMeCMSEntityContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<UserClaimDto>> GetAllUserClaimsAsync(string userid)
        {
            try
            {
                var user = await _context.AspNetUsers.FindAsync(userid);
                if (user == null)
                {
                    return null;
                }
                var claims =
                    _context.AspNetUserClaims.Where(x => x.UserId == user.Id).Select(claim => new UserClaimDto()
                    {
                        Id = claim.Id,
                        ClaimType = claim.ClaimType,
                        ClaimValue = claim.ClaimValue,
                        UserId = claim.UserId
                    });

                return claims;
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

                if (claim == null) return false;
                claim.ClaimValue = entry.ClaimValue;
                _context.AspNetUserClaims.Attach(claim);
                return await SaveAllAsync();
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

                if (claim == null) return false;
                _context.AspNetUserClaims.Remove(claim);
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
