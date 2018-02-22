using BAU.Data.Models;
using System;
using System.Collections.Generic;

namespace BAU.Data.Interfaces
{
    public interface IEngineerRepository
    {
        Engineer Find(int ID);
        IEnumerable<Engineer> Find(Func<Engineer, bool> where);
        IEnumerable<Engineer> FindAll();
        void Add(Engineer engineer);
        void Update(Engineer engineer);
        void Remove(int ID);
    }
}
