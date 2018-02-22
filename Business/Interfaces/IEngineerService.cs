using System;
using System.Collections.Generic;
using BAU.Data.Models;

namespace BAU.Business.Interfaces
{
    public interface IEngineerService
    {
        List<Engineer> FindAll();
        Engineer Find(int id);
        void Add(Engineer engineer);
        void Update(Engineer engineer);
        void Remove(int engineer);
    }
}
