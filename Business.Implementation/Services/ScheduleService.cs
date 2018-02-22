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

        
        public void PopulateNextSlot()
        {
            Func<Engineer, bool> available = (p => p.IsAvailable == true);
            List<Engineer> engineers = _engineerRepo.Find(available).OrderBy(x => Guid.NewGuid()).ToList();

            DateTime monday = DateTime.Now.AddDays(-(int)DateTime.Today.DayOfWeek
                 + (int)DayOfWeek.Monday).Date;

            Schedule s = GetSchedule(monday);

            // Get next date available

            DateTime nextDate = monday;
            int LastEngineerID = 0;
            if (s.Dates != null)
            {
                ScheduleDate lastDate = s.Dates.Last();
                nextDate = lastDate.When.AddDays(1);
                LastEngineerID = lastDate.Slots.Last().EngineerID;
            }


            if (nextDate.DayOfWeek == DayOfWeek.Saturday)
            {
                nextDate = nextDate.AddDays(2);
            }
            if (nextDate.DayOfWeek == DayOfWeek.Sunday)
            {
                nextDate = nextDate.AddDays(2);
            }


            // Assign first random Engineer ensuring it's not the same one in the previous slot
            Engineer firstEngineer = engineers.Where(e => e.IsAvailable == true && e.ID != LastEngineerID).FirstOrDefault();
            SupportSlot firstSlot = new SupportSlot
            {
                Date = nextDate,
                Slot = 1,
                EngineerID = firstEngineer.ID
            };
            _supportSlotRepo.Add(firstSlot);

            // Assign next random Engineer, ensuring it's not the same one in the previous slot
            Engineer secondEngineer = engineers.Where(e => e.IsAvailable == true && e.ID != firstSlot.EngineerID).FirstOrDefault();
            SupportSlot secondSlot = new SupportSlot
            {
                Date = nextDate,
                Slot = 2,
                EngineerID = secondEngineer.ID
            };
            _supportSlotRepo.Add(secondSlot);

        }
    }
}
