using HivelyCoreMVC.Models.ImageUploadModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public interface IImageUploadService
    {
        Task<IEnumerable<ImageUploadListItem>> GetImages();
        void SetUserId(Guid userId);
        Task<bool> CreateImage(ImageUploadCreate model);
        Task<ImageUploadDetails> GetImageById(int id);
    }
}