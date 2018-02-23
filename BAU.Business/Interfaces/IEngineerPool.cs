using System;
using System.Collections.Generic;
using BAU.Data.Models;

namespace BAU.Business.Interfaces
{
    /*  IEnginnerPool
    
        Author: Chris Fildes
        Date: 22/02/2018
        Description: Interface for a pool of Engineers 
    */

    public interface IEngineerPool
    {
        void Add(List<Engineer> engineers);
        Engineer Pull();
        int Available { get; }
        int Pullable { get; }
    }
}

