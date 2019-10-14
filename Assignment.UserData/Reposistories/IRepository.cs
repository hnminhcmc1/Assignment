using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.UserData.Reposistories
{
    public interface IRepository<T> where T:class
    {
        Task<T> Get(int id);
        void Add(T t);
    }
}
