using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepo
{
    public interface IUserRepo
    {
        Task<bool> CheckExistedEmailAddress(string email);
        Task<User> GetUserByEmailAddressAndPasswordHash(string email, string passwordHash);
        Task<string> GetTokenByUserIdAsync(int userId);
    }
}
