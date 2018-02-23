using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using BAU.Data.Models;
using BAU.Data.Interfaces;

namespace BAU.Data.EntityFramework
{
    /*  SupportSlotRepository

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Implementation of Data Layer for Support Slots. Repository implementation passed in with Dependency Injection   
    */

    public class SupportSlotRepository : DbContext, ISupportSlotRepository
    {
        DbSet<SupportSlot> SupportSlots { get; set; }
        DbSet<Engineer> Engineers { get; set; }

        public SupportSlotRepository(DbContextOptions<SupportSlotRepository> options) : base(options)
        {
        }

        public IEnumerable<SupportSlot> FindAll()
        {
            return SupportSlots;
        }

        public IEnumerable<SupportSlot> Find(Func<SupportSlot, bool> where)
        {
            return SupportSlots.Include("Engineer").Where(where);
        }


        public SupportSlot FindById(int Id)
        {
            return SupportSlots.Where(e => e.ID == Id).SingleOrDefault();
        }


        public void Add(SupportSlot slot)
        {
            SupportSlots.Add(slot);
            this.SaveChanges();

        }

        public void Update(SupportSlot slot)
        {
            var entity = SupportSlots.Find(slot.ID);
            entity.Date = slot.Date;
            entity.Slot = slot.Slot;
            entity.Engineer = slot.Engineer;
            this.SaveChanges();
        }

        public void Remove(int Id)
        {
            var entity = SupportSlots.Find(Id);
            SupportSlots.Remove(entity);
            this.SaveChanges();
        }
    }
}
