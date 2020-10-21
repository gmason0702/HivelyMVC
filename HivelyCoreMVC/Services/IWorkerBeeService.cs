using HivelyCoreMVC.Models.WorkerBeeModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public interface IWorkerBeeService
    {
        Task<bool> CreateBees(WorkerBeeCreate model);
        Task<bool> DeleteBees(int id);
        Task<IEnumerable<WorkerBeeListItem>> GetBees();
        Task<WorkerBeeDetails> GetBeesById(int id);
        void SetUserId(Guid userId);
        Task<bool> UpdateBees(WorkerBeeEdit model);
    }
}