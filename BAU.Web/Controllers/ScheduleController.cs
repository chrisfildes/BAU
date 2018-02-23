using System;
using Microsoft.AspNetCore.Mvc;
using BAU.Business.Interfaces;

namespace Web.Controllers
{
    /*  ScheduleController

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Controller for Schedule presentation layer functionality, using service passed in with Dependency Injection
        */

    public class ScheduleController : Controller
    {
        private IScheduleService service;
        public ScheduleController(IScheduleService scheduleservice)
        {
            service = scheduleservice;
        }

        public IActionResult Index()
        {
            var model = service.Get(DateTime.Now);
            return View(model);
        }

        public IActionResult Add()
        {
            service.PopulateNextSlots();
            return RedirectToAction("Index");
        }
    }
}