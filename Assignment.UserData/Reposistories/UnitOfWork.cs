using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Assignment.UserData.Contexts;

namespace Assignment.UserData.Reposistories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _context;
        private UserRepository _userRepository;
        public UnitOfWork(UserDbContext dbContext)
        {
            _context = dbContext;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public UserRepository UserRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new UserRepository(_context));
            }
        }
    }
}
