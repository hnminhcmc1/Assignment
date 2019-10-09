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
        private IUserRepository _userRepository;
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
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new UserRepository(_context));
            }
        }
    }
}
