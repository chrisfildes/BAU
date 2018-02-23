using System;
using System.Collections.Generic;
using BAU.Data.Models;

namespace BAU.Business.Interfaces
{
    public interface IScheduleService
    {
        Schedule GetSchedule(DateTime startDate);
        void Populate();
    }
}
