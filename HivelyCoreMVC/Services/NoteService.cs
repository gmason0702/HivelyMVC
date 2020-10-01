using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.NoteModels;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _context;
        private int _userId;

        public NoteService() { }

        public NoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNote(NoteCreate model)
        {
            var entity = new Note()
            {
                OwnerId = _userId,
                NoteTitle = model.NoteTitle,
                NoteDate = model.NoteDate,
                NoteContent = model.NoteContent,
                TypeOfNote = model.TypeOfNote
            };

            _context.Notes.Add(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<NoteListItem>> GetNotes()
        {
            var query = _context.Notes.Where(e => e.OwnerId == _userId)
                .Select(e => new NoteListItem
                {
                    Id = e.Id,
                    NoteTitle = e.NoteTitle,
                    NoteDate = e.NoteDate,
                    NoteContent = e.NoteContent,
                    TypeOfNote = e.TypeOfNote
                });
            return await query.ToListAsync();
        }

        public async Task<NoteDetails> GetNoteById(int id)
        {
            var entity =
                await _context.Notes
                    .FirstOrDefaultAsync(e => e.Id == id && e.OwnerId == _userId);
            return
                new NoteDetails
                {
                    Id = entity.Id,
                    NoteTitle = entity.NoteTitle,
                    NoteDate = entity.NoteDate,
                    NoteContent = entity.NoteContent,
                    TypeOfNote = entity.TypeOfNote,
                    HiveId = entity.HiveId,
                    QueenId = entity.QueenId,
                    LocationId = entity.LocationId
                };
        }
        public async Task<bool> UpdateNote(NoteEdit model)
        {
            var entity = await _context.Notes.FindAsync(model.Id);
            entity.Id = model.Id;
            entity.NoteTitle = model.NoteTitle;
            entity.NoteDate = model.NoteDate;
            entity.NoteContent = model.NoteContent;
            entity.TypeOfNote = model.TypeOfNote;
            entity.HiveId = model.HiveId;
            entity.QueenId = model.QueenId;
            entity.LocationId = model.LocationId;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteNote(int id)
        {
            var entity = await _context.Notes.FindAsync(id);
            _context.Notes.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public void SetUserId(int userId) => _userId = userId;
    }
}
