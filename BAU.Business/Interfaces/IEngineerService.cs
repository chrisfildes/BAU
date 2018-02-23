using System;
using System.Collections.Generic;
using BAU.Data.Models;

namespace BAU.Business.Interfaces
{
    /*  IEngineerService

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Interface for business layer for managing Engineers 
    */

    public interface IEngineerService
    {
        List<Engineer> FindAll();
        Engineer Find(int id);
        void Add(Engineer engineer);
        void Update(Engineer engineer);
        void Remove(int engineer);
    }
}
