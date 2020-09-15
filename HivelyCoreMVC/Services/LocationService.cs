using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.HiveModels;
using HivelyCoreMVC.Models.LocationModels;
using HivelyCoreMVC.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public class LocationService
    {
        private readonly Guid _userId;
        public LocationService()
        {

        }

        public LocationService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateLocation(LocationCreate model)
        {
            var entity = new Location()
            {
                OwnerId = _userId,
                LocationName = model.LocationName,
                City = model.City,
                State = model.State,
                Longitude = model.Longitude,
                Latitude = model.Latitude
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Locations.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<LocationListItem> GetLocations()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Locations.Where(e => e.OwnerId == _userId)
                    .Select(e => new LocationListItem
                    {
                        LocationName = e.LocationName,
                        City = e.City,
                        State = e.State,
                        Longitude = e.Longitude,
                        Latitude = e.Latitude,
                        MapLink = e.MapLink
                    });
                return query.ToArray();
            }
        }

        public LocationDetails GetLocationById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Locations.Single(e => e.Id == id);

                return
                    new LocationDetails
                    {
                        Id = entity.Id,
                        LocationName = entity.LocationName,
                        City = entity.City,
                        State = entity.State,
                        Longitude = entity.Longitude,
                        Latitude = entity.Latitude,
                        MapLink = entity.MapLink,

                        Notes = entity.Notes.Select(note => new NoteListItem
                        {
                            Id = note.Id,
                            NoteTitle = note.NoteTitle,
                            NoteDate = note.NoteDate,
                            NoteContent = note.NoteContent,
                            TypeOfNote = note.TypeOfNote,

                        }).ToList(),
                        Hives = entity.Hives.Select(r => new HiveListItem
                        {
                            Id = r.Id,
                            HiveName = r.HiveName,
                            OriginDate = r.OriginDate,
                            NumberOfDeeps = r.NumberOfDeeps,
                            HasSwarmed = r.HasSwarmed,
                            Status = r.Status,
                        }).ToList()
                    };
            }

        }
        public bool UpdateLocation(LocationEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Locations.Single(e => e.Id == model.Id);
                entity.Id = model.Id;
                entity.LocationName = model.LocationName;
                entity.City = model.City;
                entity.State = model.State;
                entity.Longitude = model.Longitude;
                entity.Latitude = model.Latitude;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteLocation(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Locations.Single(e => e.Id == id);
                ctx.Locations.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
