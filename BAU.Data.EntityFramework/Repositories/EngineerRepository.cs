using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using BAU.Data.Models;
using BAU.Data.Interfaces;

namespace BAU.Data.EntityFramework
{
    /*  EngineerRepository

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Implementation of Data Layer for Engineers. Repository implementation passed in with Dependency Injection   
    */

    public class EngineerRepository : DbContext, IEngineerRepository { 

        DbSet<Engineer> Engineers { get; set; }

        public EngineerRepository(DbContextOptions<EngineerRepository> options) : base(options)
        {
        }

        public Engineer FindById(int Id)
        {
            return Engineers.Find(Id);
        }

        public IEnumerable<Engineer> Find(Func<Engineer, bool> where)
        {
            return Engineers.Where(where);
        }

        public IEnumerable<Engineer> FindAll()
        {
            return Engineers.ToList();
        }

        public void Add(Engineer engineer)
        {
            Engineers.Add(engineer);
            this.SaveChanges();

        }

        public void Update(Engineer engineer)
        {
            var entity = Engineers.Find(engineer.ID);
            entity.FirstName = engineer.FirstName;
            entity.LastName = engineer.LastName;
            entity.IsAvailable = engineer.IsAvailable;
            this.SaveChanges();
        }

        public void Remove(int id)
        {
            var entity = Engineers.Find(id);
            Engineers.Remove(entity);
            this.SaveChanges();
        }
    }
}
