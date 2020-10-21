using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HivelyCoreMVC.Data;
using HivelyCoreMVC.Models.ImageUploadModels;
using HivelyCoreMVC.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace HivelyCoreMVC.Controllers
{
    public class ImageUploadController : Controller
    {
       
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IImageUploadService _imageUploadService;

        public ImageUploadController(IWebHostEnvironment hostEnvironment, IImageUploadService imageUploadService)
        {
  
            _hostEnvironment = hostEnvironment;
            _imageUploadService = imageUploadService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            _imageUploadService.SetUserId(userId);
            var model = await _imageUploadService.GetImages();

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadAsync(IFormFile file)
        //{
        //    using var image = Image.Load(file.OpenReadStream());
        //    image.Mutate(x => x.Resize(256, 256));
        //    await image.SaveAsync("...");
        //    return Ok();
        //}
        public IActionResult Create()
        {
            var userId = GetUserId();
            _imageUploadService.SetUserId(userId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId, Title, ImageFile")] ImageUploadCreate imageModel)
        {
            //Save image to wwwroot/image
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            imageModel.ImageName=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Image/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await imageModel.ImageFile.CopyToAsync(fileStream);
            }

            //Insert record
            if (!ModelState.IsValid) return View(imageModel);

            var userId = GetUserId();
            _imageUploadService.SetUserId(userId);

            if (await _imageUploadService.CreateImage(imageModel))
            {
                TempData["SaveResult"] = "Location was created.";
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Location could not be created.");
            return View(imageModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();
            _imageUploadService.SetUserId(userId);
            var edit = await _imageUploadService.GetImageById(id);
            var model =
                new ImageUploadEdit
                {
                    Title = edit.Title,
                    ImageFile = edit.ImageFile,
                    ImageName = edit.ImageName,
                };
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();
            _imageUploadService.SetUserId(userId);
            var detail = await _imageUploadService.GetImageById(id);
            return View(detail);
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
    }
}
