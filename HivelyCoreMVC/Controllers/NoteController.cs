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
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.Extensions.DependencyModel;

namespace HivelyCoreMVC.Controllers
{
    public class NoteController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly INoteService _service;
        private readonly ApplicationDbContext _context;
        public NoteController(IHostingEnvironment iHostingEnvironment, INoteService service, ApplicationDbContext context)
        {
            _environment = iHostingEnvironment;
            _service = service;
            _context = context;
        }

        //public NoteController(INoteService service)
        //{
        //    _service = service;
        //}
        public async Task<ActionResult> Index()
        {
            //var service = new NoteService();
            var userId = GetUserId();
            _service.SetUserId(userId);
            var model = await _service.GetNotes();

            return View(model);
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            var userId = GetUserId();
            _service.SetUserId(userId);
            return View();
        }

        // POST: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NoteCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var userId = GetUserId();
                _service.SetUserId(userId);
                
            }
            //var service = new NoteService();
            if (await _service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }

        // GET: Note/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //var service = new NoteService();
            var userId = GetUserId();
            _service.SetUserId(userId);
            var detail = await _service.GetNoteById(id);
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
        public async Task<ActionResult> Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            Guid userId = GetUserId();
            _service.SetUserId(userId);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            //var service = new NoteService();
            if (await _service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your Note was added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Note could not be added.");
            return View(model);
        }

        // GET: Note/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var userId = GetUserId();
            _service.SetUserId(userId);
            //var svc = new NoteService();
            var model = await _service.GetNoteById(id);

            return View(model);
        }

        // POST: Note/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteNote(int id)
        {
            //var service = new NoteService();
            var userId = GetUserId();
            _service.SetUserId(userId);
            await _service.DeleteNote(id);
            TempData["SaveResult"] = "Your Note was deleted. Hope it wasn't important.";

            return RedirectToAction("Index");
        }

        //[HttpPost("Note")]
        //public async Task<IActionResult> UploadAsync(List<IFormFile> file)
        //{
        //    using var image = Image.Load(file.OpenReadStream());
        //    image.Mutate(x => x.Resize(256, 256));
        //    await image.SaveAsync("...");
        //    return Ok();
        //}

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId !=null)
            {
                return Guid.Parse(userId);
            }
            return Guid.Empty;
        }

        //private NoteService CreateNoteService()
        //{
        //    var userId = Guid.Parse(User.);
        //    var service = new NoteService(userId);
        //    return service;
        //}
    }
}
