using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.WorkerBeeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public class WorkerBeeService
    {
        private readonly ApplicationDbContext _context;
        private int _userId;

        public WorkerBeeService() { }

        public WorkerBeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateBees(WorkerBeeCreate model)
        {
            var entity = new WorkerBee()
            {
                OwnerId = _userId,
                OriginLocation = model.OriginLocation,
                OriginDate = model.OriginDate,
            };
            _context.WorkerBees.Add(entity);
            var changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<IEnumerable<WorkerBeeListItem>> GetBees()
        {
            var query = _context.WorkerBees.Where(e => e.OwnerId == _userId)
                .Select(e => new WorkerBeeListItem
                {
                    OriginLocation = e.OriginLocation,
                    OriginDate = e.OriginDate,
                    Hive = e.Hive
                });
            return await query.ToListAsync();
        }

        public async Task<WorkerBeeDetails> GetBeesById(int id)
        {
            var entity = await _context.WorkerBees.FirstOrDefaultAsync(e => e.Id == id && e.OwnerId == _userId);
            return new WorkerBeeDetails()
            {
                Id = entity.Id,
                OriginLocation = entity.OriginLocation,
                OriginDate = entity.OriginDate
            };
        }

        public async Task<bool> UpdateBees(WorkerBeeEdit model)
        {

            var entity = await _context.WorkerBees.FindAsync(model.Id);
            entity.Id = model.Id;
            entity.OriginDate = model.OriginDate;
            entity.OriginLocation = model.OriginLocation;
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteBees(int id)
        {
            var entity = await _context.WorkerBees.FindAsync(id);
            _context.WorkerBees.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public void SetUserId(int userId) => _userId = userId;
    }
}
