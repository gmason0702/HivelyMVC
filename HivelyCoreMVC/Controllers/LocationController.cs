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
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace HivelyCoreMVC.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _service;
        
        public LocationController(ILocationService service)
        {
            _service = service;
        }
        public async Task<ActionResult> Index()
        {
            var userId = GetUserId();
            _service.SetUserId(userId);
            //var service = new LocationService();
            var model = await _service.GetLocations();

            return View(model);
        }

        // GET: Hive/Create
        public ActionResult Create()
        {
            var userId = GetUserId();
            _service.SetUserId(userId);
            return View();
        }

        // POST: Hive/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LocationCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var userId = GetUserId();
            _service.SetUserId(userId);
            //var service = new LocationService();
            if (await _service.CreateLocation(model))
            {
                TempData["SaveResult"] = "Location was created.";
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Location could not be created.");
            return View(model);
        }

        // GET: Hive/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //var service = new LocationService();
            var userId = GetUserId();
            _service.SetUserId(userId);
            var detail = await _service.GetLocationById(id);
            var model = new LocationEdit
            {
                LocationName = detail.LocationName,
                City = detail.City,
                State = detail.State,
                Longitude = detail.Longitude,
                Latitude = detail.Latitude,
            };
            return View(model);
        }

        // POST: Hive/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LocationEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            Guid userId = GetUserId();
            _service.SetUserId(userId);
            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            //var service = new LocationService();
            if (await _service.UpdateLocation(model))
            {
                TempData["SaveResult"] = "Location was been edited.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Location could not be edited.");
            return View(model);
        }

        // GET: Hive/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //var service = new LocationService();
            var userId = GetUserId();
            _service.SetUserId(userId);
            var model = await _service.GetLocationById(id);
            return View(model);
        }

        // POST: Hive/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteLocation(int id)
        {
            //var service = new LocationService();
            var userId = GetUserId();
            _service.SetUserId(userId);
            await _service.DeleteLocation(id);
            TempData["SaveResult"] = "Your Location has been deleted.";

            return RedirectToAction("Index");
        }

        private Guid GetUserId()
        {
            //var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                return Guid.Parse(userId);
            }
            return Guid.Empty;
        }
        //private LocationService CreateLocationService()
        //{
        //    var userId = Guid.Parse(User.Identity.Name);
        //    var service = new LocationService(userId);
        //    return service;
        //}
    }
}
