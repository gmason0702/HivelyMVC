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
using HivelyCoreMVC.Models.LocationModels;

namespace HivelyCoreMVC.Controllers
{
    public class LocationController : Controller
    {
        public ActionResult Index()
        {
            var service = new LocationService();
            var model = service.GetLocations();

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
        public ActionResult Create(LocationCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new LocationService();
            if (service.CreateLocation(model))
            {
                TempData["SaveResult"] = "Location was created.";
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Location could not be created.");
            return View(model);

        }

        // GET: Hive/Edit/5
        public ActionResult Edit(int id)
        {
            var service = new LocationService();
            var detail = service.GetLocationById(id);
            var model = new LocationEdit
            {
                LocationName = detail.LocationName,
                City = detail.City,
                State = detail.State,
                Longitude = detail.Longitude,
                Latitude = detail.Latitude,
            };
            return View();
        }

        // POST: Hive/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LocationEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = new LocationService();
            if (service.UpdateLocation(model))
            {
                TempData["SaveResult"] = "Location was been edited.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Location could not be edited.");
            return View(model);
        }


        // GET: Hive/Delete/5
        public ActionResult Delete(int id)
        {
            var service = new LocationService();
            var model = service.GetLocationById(id);
            return View(model);
        }


        // POST: Hive/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLocation(int id)
        {
            var service = new LocationService();
            service.DeleteLocation(id);
            TempData["SaveResult"] = "Your Location has been deleted.";

            return RedirectToAction("Index");
        }

        //private LocationService CreateLocationService()
        //{
        //    var userId = Guid.Parse(User.Identity.Name);
        //    var service = new LocationService(userId);
        //    return service;
        //}
    }
}
