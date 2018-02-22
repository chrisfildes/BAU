using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAU.Business.Interfaces;
using BAU.Data.Models;
using BAU.Data.Interfaces;

namespace BAU.Business.Services
{
    public class EngineerService : IEngineerService
    {
        private IEngineerRepository repo;
        public EngineerService(IEngineerRepository repository)
        {
            // Constructor Injection
            repo = repository;          
        }

        public List<Engineer> FindAll()
        {
            return repo.FindAll().ToList();
        }

        public Engineer Find(int id)
        {
            return repo.Find(id);
        }

        public void Add(Engineer e)
        {
            repo.Add(e);
        }

        public void Update(Engineer e)
        {
            repo.Update(e);
        }

        public void Remove(int id)
        {
            repo.Remove(id);
        }
        
    }
}
