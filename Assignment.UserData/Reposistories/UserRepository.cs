using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.UserData.Contexts;
using Assignment.UserData.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.UserData.Reposistories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        {
        }

        public async Task<User> FindByEmailAndPassword(string email,string password)
        {
            return (await Find(s => s.Email == email && s.Password == password)).SingleOrDefault();
        }
    }
}
