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
using HivelyCoreMVC.Models.QueenModels;
using System.Security.Claims;

namespace HivelyCoreMVC.Controllers
{
    public class QueenController : Controller
    {
        private readonly IQueenService _service;
        public QueenController(IQueenService service)
        {
            _service = service;
        }
        // GET: Queen
        public async Task<ActionResult> Index()
        {
            //var service = new QueenService();
            var userId = GetUserId();
            _service.SetUserId(userId);
            var model = await _service.GetQueens();

            return View(model);
        }

        // GET: Queen/Create
        public ActionResult Create()
        {
            var userId = GetUserId();
            _service.SetUserId(userId);
            return View();
        }

        // POST: Queen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QueenCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            //var service = new QueenService();
            var userId = GetUserId();
            _service.SetUserId(userId);
            if (await _service.CreateQueen(model))
            {
                TempData["SaveResult"] = "Your Queen was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Queen could not be created.");
            return View(model);
        }

        // GET: Queen/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //var service = new QueenService();
            var userID = GetUserId();
            _service.SetUserId(userID);
            var detail = await _service.GetQueenById(id);
            var model =
                new QueenEdit
                {
                    Age = detail.Age,
                    Color = detail.Color,
                    OriginDate = detail.OriginDate,
                    OriginLocation = detail.OriginLocation,
                };
            return View(model);
        }

        // POST: Queen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, QueenEdit model)
        {
            //var service = new QueenService();
            if (!ModelState.IsValid) return View(model);
            var userId = GetUserId();
            _service.SetUserId(userId);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            if (await _service.UpdateQueen(model))
            {
                TempData["SaveResult"] = "Your Queen was added. Long live the Queen.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your Queen could not be added.");
            return View(model);
        }

        // GET: Queen/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //var svc = new QueenService();
            var userId = GetUserId();
            _service.SetUserId(userId);

            var model = await _service.GetQueenById(id);

            return View(model);
        }

        // POST: Queen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //var service = new QueenService();
            var userId = GetUserId();
            _service.SetUserId(userId);

            await _service.DeleteQueen(id);

            TempData["SaveResult"] = "Your Queen was deleted. May she return soon.";

            return RedirectToAction("Index");
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId!=null)
            {
                return Guid.Parse(userId);
            }
            return Guid.Empty;
        }
        //private QueenService CreateQueenService()
        //{
        //    var userId = Guid.Parse(User.Identity.Name);
        //    var service = new QueenService(userId);
        //    return service;
        //}
    }
}
