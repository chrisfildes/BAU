using BAU.Data.Models;
using System;
using System.Collections.Generic;

namespace BAU.Data.Interfaces
{
    public interface ISupportSlotRepository
    {
        SupportSlot Find(int ID);
        IEnumerable<SupportSlot> Find(Func<SupportSlot, bool> where);
        void Add(SupportSlot slot);
        void Update(SupportSlot slot);
        void Remove(int ID);
    }
}
