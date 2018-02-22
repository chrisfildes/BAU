using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BAU.Data.Models
{
    public class SupportSlot
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Slot { get; set; }
        public int EngineerID { get; set; }
        public virtual Engineer Engineer { get; set; }
    }
}
