using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BAU.Data.Models
{
    public class ScheduleSlot
    {
        public int EngineerID;
        public string EngineerName;
    }

    public class ScheduleDate
    {
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime When;
        public List<ScheduleSlot> Slots;
    }

    public class Schedule
    {
        public List<ScheduleDate> Dates;
    }
}
