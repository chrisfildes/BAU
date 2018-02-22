using System;
using Microsoft.AspNetCore.Mvc;
using BAU.Business.Interfaces;

namespace Web.Controllers
{
    public class ScheduleController : Controller
    {
        private IScheduleService service;
        public ScheduleController(IScheduleService scheduleservice)
        {
            service = scheduleservice;
        }

        public IActionResult Index()
        {
            var model = service.GetSchedule(DateTime.Now);
            return View(model.Dates);
        }

        public IActionResult Add()
        {
            service.Populate(1);
            return RedirectToAction("Index");
        }
    }
}