using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAU.Business.Interfaces;
using BAU.Data.Models;
using BAU.Data.Interfaces;

namespace BAU.Business.Services
{
    public class EngineerPool : IEngineerPool
    {
        private List<Engineer> availableEngineers;
        private Stack<Engineer> currentEngineers;

        public EngineerPool()
        {
            
        }

        public void Add(List<Engineer> engineers)
        {
            availableEngineers = engineers;
        }

        private void ResetPool()
        {
            currentEngineers = new Stack<Engineer>(availableEngineers.OrderBy(x => Guid.NewGuid()));
        }

        public Engineer PullRandom()
        {
            if (currentEngineers == null)
            {
                ResetPool();
            }
            Engineer nextEngineer = currentEngineers.Pop();
            return nextEngineer;
        }

        public int Available
        {
            get
            {
                return availableEngineers.Count;
            }
        }

        public int Pullable
        {
            get
            {
                return currentEngineers.Count;
            }
        }

    }

}
