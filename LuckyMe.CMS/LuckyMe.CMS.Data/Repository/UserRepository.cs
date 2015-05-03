using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LuckyMe.CMS.Data.Repository.Interfaces;
using LuckyMe.CMS.Entity.DTO;

namespace LuckyMe.CMS.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LuckyMeCMSEntityContext _context;
        public UserRepository(LuckyMeCMSEntityContext context)
        {
            _context = context;
        }

        public IQueryable<UserDTO> GetAllUsers()
        {
            var users = _context.AspNetUsers;

            var userList = users.Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserProviders =
                    _context.AspNetUserLogins.Where(x => x.UserId == user.Id).Select(p => new UserProviderDTO
                    {
                        LoginProvider = p.LoginProvider,
                        ProviderKey = p.ProviderKey,
                        UserId = p.UserId
                    }).ToList()
            });

            return userList;
        }

        public UserDTO GetUserById(string id)
        {
            return GetAllUsers().FirstOrDefault(x => x.Id == id);
        }

        public bool InsertUserExternalLoginEntry(UserProviderDTO entry)
        {
            try
            {
                var record = _context.AspNetUserLogins.Create();
                record.UserId = entry.UserId;
                record.LoginProvider = entry.LoginProvider;
                record.ProviderKey = entry.ProviderKey;
                _context.AspNetUserLogins.Add(record);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUserExternalLoginEntry(UserProviderDTO entry)
        {
            try
            {
                var userlogin =
                    _context.AspNetUserLogins.FirstOrDefault(
                        x =>
                            x.UserId == entry.UserId && x.LoginProvider == entry.LoginProvider);

                if (userlogin != null)
                {
                    userlogin.ProviderKey = entry.ProviderKey;
                    _context.AspNetUserLogins.Attach(userlogin);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUserExternalLoginEntry(UserProviderDTO entry)
        {
            try
            {
                var userlogin =
                    _context.AspNetUserLogins.FirstOrDefault(
                        x =>
                            x.UserId == entry.UserId && x.LoginProvider == entry.LoginProvider);

                if (userlogin != null)
                {
                    userlogin.ProviderKey = entry.ProviderKey;
                    _context.AspNetUserLogins.Remove(userlogin);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }


        public async Task<IQueryable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _context.AspNetUsers.ToListAsync();

            var userList = users.Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserProviders =
                    _context.AspNetUserLogins.Where(x => x.UserId == user.Id).Select(p => new UserProviderDTO
                    {
                        LoginProvider = p.LoginProvider,
                        ProviderKey = p.ProviderKey,
                        UserId = p.UserId
                    }).ToList()
            });

            return userList.AsQueryable();
        }
    }
}


