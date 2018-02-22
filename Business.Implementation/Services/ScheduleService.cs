using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAU.Business.Interfaces;
using BAU.Data.Models;
using BAU.Data.Interfaces;

namespace BAU.Business.Services
{
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

        public Schedule GetSchedule(DateTime startDate)
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

        public DateTime GetStartDate(SupportSlot lastSlot)
        {
            if (lastSlot == null)
            {
                return DateTime.Now.AddDays(-(int)DateTime.Today.DayOfWeek
                 + (int)DayOfWeek.Monday).Date;
            }
            return lastSlot.Date.AddDays(3);
        }


        public void Populate(int noWeeks)
        {
            // Work out which week to populate next, and get who was last engineer to do a slot
            SupportSlot lastSlot = _supportSlotRepo.Find(e => e.Date >= DateTime.Now).OrderByDescending(e => e.Date).ThenByDescending(e => e.Slot).Take(1).SingleOrDefault();
            DateTime currentDate = GetStartDate(lastSlot);
            int lastEngineerID = (lastSlot == null ? 0 : lastSlot.EngineerID);

            // Create an pool of engineers to randomly pull from. Will not repeat engineer until all pulled.
            EngineerPool engineerPool = new EngineerPool();
            engineerPool.Add(_engineerRepo.Find(e => e.IsAvailable == true).ToList());

            for (int w = 0; w < noWeeks; w++)
            {
                for (int d = 0; d < 5; d++)
                {
                    for (int s = 0; s < 2; s++)
                    {
                        Engineer e = engineerPool.PullRandom();
                        SupportSlot slot = new SupportSlot
                        {
                            EngineerID = e.ID,
                            Date = currentDate,
                            Slot = s
                        };

                        _supportSlotRepo.Add(slot);

                    }
                    currentDate = currentDate.AddDays(1);
                }
            }

        }
    }
}
