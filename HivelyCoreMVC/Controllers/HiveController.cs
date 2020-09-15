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
using System.Security.Claims;
using HivelyCoreMVC.Models.HiveModels;

namespace HivelyCoreMVC.Controllers
{
    public class HiveController : Controller
    {
        public ActionResult Index()
        {
            var service = new HiveService();
            var model = service.GetHives();

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
        public ActionResult Create(HiveCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new HiveService();
            if (service.CreateHive(model))
            {
                TempData["SaveResult"] = "Your hive was created. May the flow be aplenty.";
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Hive could not be created.");
            return View(model);

        }

        // GET: Hive/Edit/5
        public ActionResult Edit(int id)
        {
            var service = new HiveService();
            var detail = service.GetHiveById(id);
            var model = new HiveEdit
            {
                HiveName = detail.HiveName,
                OriginDate = detail.OriginDate,
                NumberOfDeeps = detail.NumberOfDeeps,
                HasSwarmed = detail.HasSwarmed,
                Status = detail.Status,
                LocationId = detail.LocationId
            };
            return View();
        }

        // POST: Hive/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HiveEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = new HiveService();
            if (service.UpdateHive(model))
            {
                TempData["SaveResult"] = "Your Hive was been edited.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Hive could not be edited.");
            return View(model);
        }


        // GET: Hive/Delete/5
        public ActionResult Delete(int id)
        {
            var service = new HiveService();
            var model = service.GetHiveById(id);
            return View(model);
        }


        // POST: Hive/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteHive(int id)
        {
            var service = new HiveService();
            service.DeleteHive(id);
            TempData["SaveResult"] = "Your Hive has been deleted. Rest in Pollen.";

            return RedirectToAction("Index");
        }

        //private HiveService CreateHiveService()
        //{
        //    var userId = Guid.Parse(User.Identity.Name);
        //    var service = new HiveService(userId);
        //    return service;
        //}
    }
}
