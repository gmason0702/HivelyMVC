using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Services;
using HivelyCoreMVC.Models.WorkerBeeModels;

namespace HivelyCoreMVC.Controllers
{
    public class WorkerBeeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var service = new WorkerBeeService(); 
            var model = await service.GetBees();

            return View(model);
        }


        // GET: Hive/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hive/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkerBeeCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new WorkerBeeService();
            if (await service.CreateBees(model))
            {
                TempData["SaveResult"] = "Your worker bees were added. May they polinate.";
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Worker bees could not be added.");
            return View(model);
        }

        // GET: Hive/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var service = new WorkerBeeService();
            var detail = await service.GetBeesById(id);
            var model = new WorkerBeeEdit
            {
                OriginDate = detail.OriginDate,
                OriginLocation = detail.OriginLocation,
            };
            return View();
        }

        // POST: Hive/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, WorkerBeeEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = new WorkerBeeService();
            if (await service.UpdateBees(model))
            {
                TempData["SaveResult"] = "Your bees were been edited.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your bees could not be edited.");
            return View(model);
        }

        // GET: Hive/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var service = new WorkerBeeService();
            var model = await service.GetBeesById(id);
            return View(model);
        }

        // POST: Hive/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteHive(int id)
        {
            var service = new WorkerBeeService();
            await service.DeleteBees(id);
            TempData["SaveResult"] = "Your Hive has been deleted. Rest in Pollen.";

            return RedirectToAction("Index");
        }

        //private WorkerBeeService CreateWorkerBeeService()
        //{
        //    var userId = Guid.Parse(User.Identity.GetUserId()); 
        //    var service = new WorkerBeeService(userId);
        //    return service;
        //}
    }
}
