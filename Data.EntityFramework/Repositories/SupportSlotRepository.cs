using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using BAU.Data.Models;
using BAU.Data.Interfaces;

namespace BAU.Data.EntityFramework
{
    public class SupportSlotRepository : DbContext, ISupportSlotRepository
    {
        DbSet<SupportSlot> SupportSlots { get; set; }
        DbSet<Engineer> Engineers { get; set; }
        
        public SupportSlotRepository(DbContextOptions<SupportSlotRepository> options) : base(options) {
        }

        public SupportSlot Find(int ID)
        {
            return SupportSlots.Where(e => e.ID == ID).SingleOrDefault();
        }

        public IEnumerable<SupportSlot> Find(Func<SupportSlot, bool> where)
        {
            return SupportSlots.Include("Engineer").Where(where);
        }

        public IEnumerable<SupportSlot> FindAll()
        {
            return SupportSlots.ToList();
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

        public void Remove(int id)
        {
            var entity = SupportSlots.Find(id);
            SupportSlots.Remove(entity);
            this.SaveChanges();
        }


    }
}
