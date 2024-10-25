using DataAccess.Entity;
using DataAccess.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly WashShopContext _dbContext;
        private readonly UserRepo _userRepo;
        public UserRepo(WashShopContext context, UserRepo userRepo)
        {
            _dbContext = context;
            _userRepo = userRepo;
        }
        public async Task<bool> CheckExistedEmailAddress(string email)
        {
            return await _dbContext.Users.AnyAsync(e => e.Email == email);
        }

        public async Task<User> GetUserByEmailAddressAndPasswordHash(string email, string passwordHash)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == email && e.Password == passwordHash);
            return user;
        }
    }
}
