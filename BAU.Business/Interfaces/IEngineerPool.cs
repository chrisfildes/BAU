using System;
using System.Collections.Generic;
using BAU.Data.Models;

namespace BAU.Business.Interfaces
{
    public interface IEngineerPool
    {
        void Add(List<Engineer> engineers);
        Engineer PullRandom();
        int Available { get; }
        int Pullable { get; }
    }

}

