using HivelyCoreMVC.Models.HiveModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public interface IHiveService
    {
        Task<bool> CreateHive(HiveCreate model);
        Task<bool> DeleteHive(int id);
        Task<HiveDetails> GetHiveById(int id);
        Task<IEnumerable<HiveListItem>> GetHives();
        void SetUserId(Guid userId);
        Task<bool> UpdateHive(HiveEdit model);
    }
}