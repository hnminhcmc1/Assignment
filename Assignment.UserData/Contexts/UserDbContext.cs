
using Assignment.UserData.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.UserData.Contexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
