using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.UserData.Contexts;
using Assignment.UserData.Interfaces;
using Assignment.UserData.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.UserData.Reposistories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserDbContext _userDbContext;
        public UserRepository(UserDbContext userDbContext) : base(userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<User> FindByEmailAndPassword(string email,string password)
        {
            return await _userDbContext.Users.SingleOrDefaultAsync(s => s.Email == email && s.Password == password);
        }

        public bool CheckExistUser(string email,string name)
        {
            return _userDbContext.Users.Any(s => s.Email == email || s.Name == name);
        }
    }
}
