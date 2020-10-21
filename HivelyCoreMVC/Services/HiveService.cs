using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.HiveModels;
using HivelyCoreMVC.Models.NoteModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public class HiveService : IHiveService
    {
        private readonly ApplicationDbContext _context;
        private Guid _userId;

        public HiveService() { }
        public HiveService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateHive(HiveCreate model)
        {
            var entity = new Hive()
            {
                OwnerId = _userId,
                HiveName = model.HiveName,
                OriginDate = model.OriginDate,
                NumberOfDeeps = model.NumberOfDeeps,
                Status = model.Status,
                LocationId = model.LocationId
            };
            _context.Hives.Add(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<HiveListItem>> GetHives()
        {
            var query = _context.Hives
                .Select(e => new HiveListItem
                {
                    HiveName = e.HiveName,
                    OriginDate = e.OriginDate,
                    NumberOfDeeps = e.NumberOfDeeps,
                    HasSwarmed = e.HasSwarmed,
                    Status = e.Status
                });
            return await query.ToListAsync();
        }

        public async Task<HiveDetails> GetHiveById(int id)
        {

            var entity = await _context.Hives.FirstOrDefaultAsync(e => e.Id == id && e.OwnerId == _userId);
            if (entity is null)
            {
                return null;
            }
            return new HiveDetails()
            {
                Id = entity.Id,
                HiveName = entity.HiveName,
                OriginDate = entity.OriginDate,
                NumberOfDeeps = entity.NumberOfDeeps,
                HasSwarmed = entity.HasSwarmed,
                Status = entity.Status,
                LocationName = entity.Locations.LocationName,
                QueenName = entity.Queens.QueenName,

                Notes = entity.Notes.Select(n => new NoteListItem
                {
                    Id = n.Id,
                    NoteTitle = n.NoteTitle,
                    NoteDate = n.NoteDate,
                    NoteContent = n.NoteContent,
                    TypeOfNote = n.TypeOfNote
                }).ToList()
            };
        }
        public async Task<bool> UpdateHive(HiveEdit model)
        {
            var entity = await _context.Hives.FindAsync(model.Id);
            entity.Id = model.Id;
            entity.HiveName = model.HiveName;
            entity.OriginDate = model.OriginDate;
            entity.NumberOfDeeps = model.NumberOfDeeps;
            entity.HasSwarmed = model.HasSwarmed;
            entity.Status = model.Status;
            entity.LocationId = model.LocationId;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteHive(int id)
        {
            var entity = await _context.Hives.FindAsync(id);
            _context.Hives.Remove(entity);
            return _context.SaveChanges() == 1;
        }

        public void SetUserId(Guid userId) => _userId = userId;
    }
}
