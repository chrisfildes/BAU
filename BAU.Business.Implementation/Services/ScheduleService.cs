using System;
using System.Collections.Generic;
using System.Linq;
using BAU.Business.Interfaces;
using BAU.Data.Models;
using BAU.Data.Interfaces;

namespace BAU.Business.Services
{
    /*  ScheduleService

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Implementation of Business Layer for Schedule. Repository implementation passed in with Dependency Injection   
    */

    public class ScheduleService : IScheduleService
    {
        private ISupportSlotRepository _supportSlotRepo;
        private IEngineerRepository _engineerRepo;

        public ScheduleService(ISupportSlotRepository supportSlotRepo, IEngineerRepository engineerRepo)
        {
            // Constructor Injection
            _supportSlotRepo = supportSlotRepo;
            _engineerRepo = engineerRepo;
        }

        public Schedule Get(DateTime startDate)
        {
            DateTime monday = startDate.AddDays(-(int)DateTime.Today.DayOfWeek
                 + (int)DayOfWeek.Monday).Date;

            Func<SupportSlot, bool> where = (p => p.Date >= monday);

            List<SupportSlot> slots = _supportSlotRepo.Find(where).ToList().OrderBy(e => e.Date).ThenBy(e => e.Slot).ToList();

            Schedule schedule = new Schedule();

            if (slots.Count > 0)
            {

                schedule.Dates = new List<ScheduleDate>();
                DateTime endDate = slots.Max(e => e.Date);
                DateTime currentDate = monday;
                while (currentDate <= endDate)
                {
                    if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                    {

                        ScheduleDate newCalendarDate = new ScheduleDate();
                        newCalendarDate.When = currentDate;
                        newCalendarDate.Slots = new List<ScheduleSlot>();

                        List<SupportSlot> slotstoday = slots.Where(e => e.Date == currentDate).ToList();
                        foreach (var slot in slotstoday)
                        {
                            newCalendarDate.Slots.Add(new ScheduleSlot { EngineerID = slot.Engineer.ID, EngineerName = slot.Engineer.FirstName + " " + slot.Engineer.LastName });
                        }


                        schedule.Dates.Add(newCalendarDate);

                    }
                    currentDate = currentDate.AddDays(1);
                }

            }

            return schedule;
        }

        DateTime GetNextEmptyWeekStartDate(SupportSlot lastSlot)
        {
            if (lastSlot == null)
            {
                // return Monday of current week, if this is the
                return DateTime.Now.AddDays(-(int)DateTime.Today.DayOfWeek
                 + (int)DayOfWeek.Monday).Date;
            }
            return lastSlot.Date.AddDays(3);
        }

        public void PopulateNextSlots()
        {
            // Setting the number of weeks to 2 in this implementation. 
            // This ensures for 10 of less Engineers they are added in for at least one full day in each 2 week slot. 
            // For more than 10 Engineers you can't guarantee that, as not enough slots!

            int noWeeks = 2;

            // Work out which week to populate next, and get who was last engineer to do a slot:

            // 1. Get last slot populated

            SupportSlot lastSlot = _supportSlotRepo.Find(e => e.Date >= DateTime.Now).OrderByDescending(e => e.Date).ThenByDescending(e => e.Slot).Take(1).SingleOrDefault();

            // 2 . Get start of week for next empty week

            DateTime currentDate = GetNextEmptyWeekStartDate(lastSlot);

            // 3. Get last engineer (if one) to ensure they aren't added in consecutive slot

            int lastEngineerID = (lastSlot == null ? 0 : lastSlot.EngineerID);

            // Create an pool of engineers to randomly pull from. Will not repeat engineer until all pulled.

            EngineerPool engineerPool = new EngineerPool();
            engineerPool.Add(_engineerRepo.Find(e => e.IsAvailable == true).ToList());

            // Go through each week, each day and each slot getting next available engineer for each slot.

            for (int week = 0; week < noWeeks; week++)
            {
                for (int day = 0; day < 5; day++)
                {
                    for (int slot = 0; slot < 2; slot++)
                    {
                        Engineer e = engineerPool.Pull();
                        SupportSlot newSlot = new SupportSlot
                        {
                            EngineerID = e.ID,
                            Date = currentDate,
                            Slot = slot
                        };

                        _supportSlotRepo.Add(newSlot);

                    }
                    currentDate = currentDate.AddDays(1);
                }
                currentDate = currentDate.AddDays(2);
            }

        }
    }
}
