using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAU.Business.Interfaces;
using BAU.Data.Models;
using BAU.Data.Interfaces;

namespace BAU.Business.Services
{
    /*  EngineerService

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Implementation of Business Layer for Engineers. Repository implementation passed in with Dependency Injection   
    */

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
            return repo.FindById(id);
        }

        public void Add(Engineer e)
        {
            repo.Add(e);
        }

        public void Update(Engineer e)
        {
            repo.Update(e);
        }

        public void Remove(int Id)
        {
            repo.Remove(Id);
        }
    }
}
