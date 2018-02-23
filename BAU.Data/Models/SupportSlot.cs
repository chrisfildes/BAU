using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BAU.Data.Models
{
    /*  SupportSlot

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Data Model for Support Slot
    */

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
