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

namespace HivelyCoreMVC.Controllers
{
    public class QueenController : Controller
    {
        // GET: Queen
        public ActionResult Index()
        {
            var service = new QueenService();
            var model = service.GetQueens();

            return View(model);
        }


        // GET: Queen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Queen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QueenCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new QueenService();
            if (service.CreateQueen(model))
            {
                TempData["SaveResult"] = "Your Queen was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Queen could not be created.");
            return View(model);
        }

        // GET: Queen/Edit/5
        public ActionResult Edit(int id)
        {
            var service = new QueenService();
            var detail = service.GetQueenById(id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QueenEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = new QueenService();
            if (service.UpdateQueen(model))
            {
                TempData["SaveResult"] = "Your Queen was added. Long live the Queen.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Queen could not be added.");
            return View(model);
        }

        // GET: Queen/Delete/5
        public ActionResult Delete(int id)
        {
            var svc = new QueenService();
            var model = svc.GetQueenById(id);

            return View(model);
        }

        // POST: Queen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var service = new QueenService();
            service.DeleteQueen(id);
            TempData["SaveResult"] = "Your Queen was deleted. May she return soon.";

            return RedirectToAction("Index");
        }

        //private QueenService CreateQueenService()
        //{
        //    var userId = Guid.Parse(User.Identity.Name);
        //    var service = new QueenService(userId);
        //    return service;
        //}
    }
}
