using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Assignment.UserData.Models;

namespace Assignment.Business.Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> Authenticate(string email, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserById(int id);
    }
}
