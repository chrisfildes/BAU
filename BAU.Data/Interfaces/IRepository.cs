using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAU.Data.Interfaces
{
    /*  IEngineerRepository

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Interface for generic Repository
    */

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
