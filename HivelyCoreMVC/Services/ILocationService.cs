using HivelyCoreMVC.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public interface ILocationService
    {
        Task<bool> CreateLocation(LocationCreate model);
        Task<bool> DeleteLocation(int id);
        Task<LocationDetails> GetLocationById(int id);
        Task<IEnumerable<LocationListItem>> GetLocations();
        void SetUserId(Guid userId);
        Task<bool> UpdateLocation(LocationEdit model);
    }
}