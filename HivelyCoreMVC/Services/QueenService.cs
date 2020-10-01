using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.HiveModels;
using HivelyCoreMVC.Models.NoteModels;
using HivelyCoreMVC.Models.QueenModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public class QueenService
    {
        private readonly ApplicationDbContext _context;
        private int _userId;

        public QueenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public QueenService() { }

        public async Task<bool> CreateQueen(QueenCreate model)
        {
            var entity = new Queen()
            {
                OwnerId = _userId,
                Age = model.Age,
                Color = model.Color,
                OriginDate = model.OriginDate,
                OriginLocation = model.OriginLocation,
            };

            _context.Queens.Add(entity);
            var changes = await _context.SaveChangesAsync();
            return changes == 1;
        }

        public async Task<IEnumerable<QueenListItem>> GetQueens()
        {
            var query = _context.Queens.Where(e => e.OwnerId == _userId)
                .Select(e => new QueenListItem
                {
                    Age = e.Age,
                    Color = e.Color,
                    OriginDate = e.OriginDate,
                    OriginLocation = e.OriginLocation,
                    Notes = e.Notes
                });
            return await query.ToListAsync();
        }

        public async Task<QueenDetails> GetQueenById(int id)
        {
            var entity =
                await _context
                    .Queens
                    .FirstOrDefaultAsync(e => e.Id == id && e.OwnerId == _userId);
            if (entity is null)
            {
                return null;
            }
            return
                new QueenDetails
                {
                    Id = entity.Id,
                    Age = entity.Age,
                    Color = entity.Color,
                    OriginDate = entity.OriginDate,
                    OriginLocation = entity.OriginLocation,

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
        public async Task<bool> UpdateQueen(QueenEdit model)
        {

            var entity = await _context.Queens.FindAsync(model.Id);
            entity.Id = model.Id;
            entity.Age = model.Age;
            entity.Color = model.Color;
            entity.OriginDate = model.OriginDate;
            entity.OriginLocation = model.OriginLocation;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteQueen(int id)
        {

            var entity = await _context.Queens.FindAsync(id);
            if (entity?.OwnerId !=_userId)
            {
                return false;
            }
            _context.Queens.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public void SetUserId(int userId) => _userId = userId;
    }
}
