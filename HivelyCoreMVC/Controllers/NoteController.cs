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
using HivelyCoreMVC.Models.NoteModels;
using System.Security.Claims;

namespace HivelyCoreMVC.Controllers
{
    public class NoteController : Controller
    {
        public ActionResult Index()
        {
            var service = new NoteService();
            var model = service.GetNotes();

            return View(model);
        }


        // GET: Note/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new NoteService();
            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }

        // GET: Note/Edit/5
        public ActionResult Edit(int id)
        {
            var service = new NoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteTitle = detail.NoteTitle,
                    NoteDate = detail.NoteDate,
                    NoteContent = detail.NoteContent,
                    TypeOfNote = detail.TypeOfNote,
                    HiveId = detail.HiveId,
                    QueenId = detail.QueenId,
                    LocationId = detail.LocationId
                };
            return View(model);
        }

        // POST: Note/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = new NoteService();
            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your Note was added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Note could not be added.");
            return View(model);
        }

        // GET: Note/Delete/5
        public ActionResult Delete(int id)
        {
            var svc = new NoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        // POST: Note/Delete/5
        [HttpPost]
        public ActionResult DeleteNote(int id)
        {
            var service = new NoteService();
            service.DeleteNote(id);
            TempData["SaveResult"] = "Your Note was deleted. Hope it wasn't important.";

            return RedirectToAction("Index");
        }


        //private NoteService CreateNoteService()
        //{
        //    var userId = Guid.Parse(User.);
        //    var service = new NoteService(userId);
        //    return service;
        //}
    }
}
