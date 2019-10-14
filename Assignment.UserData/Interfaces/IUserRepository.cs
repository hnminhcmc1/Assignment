using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Assignment.UserData.Models;

namespace Assignment.UserData.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByEmailAndPassword(string email, string password);
        bool CheckExistUser(string name);
    }
}
