using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAU.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> Find(Func<T, bool> where);
        T FindById(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(int id);
    }
}
