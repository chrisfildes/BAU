using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BAU.Business.Interfaces;
using BAU.Data.Models;
using BAU.Website.Models;

namespace Web.Controllers
{
    public class EngineersController : Controller
    {
        private IEngineerService service;
        public EngineersController(IEngineerService engineerservice)
        {
            service = engineerservice;
        }

        public IActionResult Index()
        {
            var  model = service.FindAll();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EngineerCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var engineer = new Engineer()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsAvailable = model.IsAvailable
                };
                service.Add(engineer);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engineer = service.Find(id ?? 0);
            if (engineer == null)
            {
                return NotFound();
            }

            EngineerEditModel model = new EngineerEditModel
            {
                ID = engineer.ID,
                FirstName = engineer.FirstName,
                LastName = engineer.LastName,
                IsAvailable = engineer.IsAvailable
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditConfirmed(EngineerEditModel model)
        {
            Engineer engineer = service.Find(model.ID);
            engineer.FirstName = model.FirstName;
            engineer.LastName = model.LastName;
            engineer.IsAvailable = model.IsAvailable;
            service.Update(engineer);
            return RedirectToAction(nameof(Index));
        }
    }
}
