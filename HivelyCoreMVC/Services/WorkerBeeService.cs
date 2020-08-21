using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.WorkerBeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public class WorkerBeeService
    {
        private readonly Guid _userId;

        public WorkerBeeService()
        {

        }
        public WorkerBeeService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBees(WorkerBeeCreate model)
        {

            var entity = new WorkerBee()
            {
                OwnerId = _userId,
                OriginLocation = model.OriginLocation,
                OriginDate = model.OriginDate,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.WorkerBees.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }

        public IEnumerable<WorkerBeeListItem> GetBees()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.WorkerBees.Where(e => e.OwnerId == _userId)
                    .Select(e => new WorkerBeeListItem
                    {
                        OriginLocation = e.OriginLocation,
                        OriginDate = e.OriginDate,
                        Hive = e.Hive
                    });
                return query.ToArray();
            }
        }

        public WorkerBeeDetails GetBeesById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.WorkerBees.Single(e => e.Id == id);
                return new WorkerBeeDetails()
                {
                    Id = entity.Id,
                    OriginLocation = entity.OriginLocation,
                    OriginDate = entity.OriginDate

                };
            }
        }
        public bool UpdateBees(WorkerBeeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.WorkerBees.Single(e => e.Id == model.Id);
                entity.Id = model.Id;
                entity.OriginDate = model.OriginDate;
                entity.OriginLocation = model.OriginLocation;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBees(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.WorkerBees.Single(e => e.Id == id);
                ctx.WorkerBees.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
