using System;
using System.Collections.Generic;
using BAU.Data.Models;

namespace BAU.Business.Interfaces
{
    /*  IScheduleService

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Interface for Business Layer of Engineer Schedule
    */

    public interface IScheduleService
    {
        Schedule Get(DateTime startDate);
        void PopulateNextSlots();
    }
}
