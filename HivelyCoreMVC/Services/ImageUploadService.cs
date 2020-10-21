using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.ImageUploadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly ApplicationDbContext _context;
        private Guid _userId;
        public ImageUploadService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ImageUploadListItem>> GetImages()
        {
            var locationQuery = _context.Images.Where(e => e.OwnerId == _userId)
                    .Select(e => new ImageUploadListItem
                    {
                        ImageId = e.ImageId,
                        Title = e.Title,
                        ImageName = e.ImageName,

                    });
            return await locationQuery.ToArrayAsync();
        }
        public async Task<bool> CreateImage(ImageUploadCreate model)
        {
            var entity = new ImageUpload()
            {
                ImageId = model.ImageId,
                OwnerId = _userId,
                Title = model.Title,
                ImageFile = model.ImageFile,
                ImageName = model.ImageName
      
            };
            _context.Images.Add(entity);

            var changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<ImageUploadDetails> GetImageById(int id)
        {
            var entity = await _context.Images
                .FirstOrDefaultAsync(e => e.ImageId == id && e.OwnerId == _userId);
            if (entity is null)
            {
                return null;
            }
            return new ImageUploadDetails
            {
                ImageId = entity.ImageId,
                ImageFile = entity.ImageFile,
                ImageName = entity.ImageName,
                Title = entity.Title
            };
        }
        public void SetUserId(Guid userId) => _userId = userId;
    }
}
