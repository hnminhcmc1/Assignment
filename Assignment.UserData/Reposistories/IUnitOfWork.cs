using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.UserData.Reposistories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChanges();
        IUserRepository UserRepository { get; }
    }
}
