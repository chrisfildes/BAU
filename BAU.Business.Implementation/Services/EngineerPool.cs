using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAU.Business.Interfaces;
using BAU.Data.Models;
using BAU.Data.Interfaces;

namespace BAU.Business.Services
{
    /*  EnginnerPool

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Implementation for a pool of Engineers (as a Stack). 
       
        Engineers are pulled ar random, and no Engineer is pulled again until all Engineers have been pulled.
    */

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

        private void Reset()
        {
            // Create stack by randomising available Engineers

            currentEngineers = new Stack<Engineer>(availableEngineers.OrderBy(x => Guid.NewGuid()));
        }

        public Engineer Pull()
        {
            if (currentEngineers == null || currentEngineers.Count == 0)
            {
                Reset();
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
