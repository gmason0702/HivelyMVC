using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.HiveModels;
using HivelyCoreMVC.Models.LocationModels;
using HivelyCoreMVC.Models.NoteModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        private Guid _userId;

        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public LocationService() { }

        public async Task<bool> CreateLocation(LocationCreate model)
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
            _context.Locations.Add(entity);

            var changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<IEnumerable<LocationListItem>> GetLocations()
        {
            var locationQuery = _context.Locations.Where(e => e.OwnerId == _userId)
                    .Select(e => new LocationListItem
                    {
                        LocationName = e.LocationName,
                        City = e.City,
                        State = e.State,
                        Longitude = e.Longitude,
                        Latitude = e.Latitude,
                        MapLink = e.MapLink
                    });
            return await locationQuery.ToArrayAsync();
        }

        public async Task<LocationDetails> GetLocationById(int id)
        {
            var entity = await _context.Locations
                           .FirstOrDefaultAsync(e => e.Id == id && e.OwnerId == _userId);
            if (entity is null)
            {
                return null;
            }
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

        public async Task<bool> UpdateLocation(LocationEdit model)
        {
            var entity = await _context.Locations.FindAsync(model.Id);
            if (entity?.OwnerId != _userId)
            {
                entity.Id = model.Id;
                entity.LocationName = model.LocationName;
                entity.City = model.City;
                entity.State = model.State;
                entity.Longitude = model.Longitude;
                entity.Latitude = model.Latitude;
            }
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteLocation(int id)
        {
            var entity = await _context.Locations.FindAsync(id);
            if (entity?.OwnerId != _userId)
            {
                return false;
            }
            _context.Locations.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public void SetUserId(Guid userId) => _userId = userId;
    }
}
