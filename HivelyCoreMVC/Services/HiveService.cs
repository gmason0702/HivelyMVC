using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.HiveModels;
using HivelyCoreMVC.Models.NoteModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public class HiveService
    {
        private readonly Guid _userId;

        public HiveService()
        {

        }
        public HiveService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateHive(HiveCreate model)
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

            using (var ctx = new ApplicationDbContext()) //only working with empty ctor but won't actually add/save 
            {
                ctx.Hives.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }

        public IEnumerable<HiveListItem> GetHives()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Hives.Where(e => e.OwnerId == _userId)
                    .Select(e => new HiveListItem
                    {
                        HiveName = e.HiveName,
                        OriginDate = e.OriginDate,
                        NumberOfDeeps = e.NumberOfDeeps,
                        HasSwarmed = e.HasSwarmed,
                        Status = e.Status

                    });
                return query.ToArray();
            }
        }

        public HiveDetails GetHiveById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Hives.Single(e => e.Id == id);
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
        }
        public bool UpdateHive(HiveEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Hives.Single(e => e.Id == model.Id);
                entity.Id = model.Id;
                entity.HiveName = model.HiveName;
                entity.OriginDate = model.OriginDate;
                entity.NumberOfDeeps = model.NumberOfDeeps;
                entity.HasSwarmed = model.HasSwarmed;
                entity.Status = model.Status;
                entity.LocationId = model.LocationId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteHive(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Hives.Single(e => e.Id == id);
                ctx.Hives.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
