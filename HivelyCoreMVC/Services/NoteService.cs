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
        private readonly Guid _userId;

        public NoteService()
        {
        }

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            var entity = new Note()
            {
                OwnerId = _userId,
                NoteTitle = model.NoteTitle,
                NoteDate = model.NoteDate,
                NoteContent = model.NoteContent,
                TypeOfNote = model.TypeOfNote
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Notes.Where(e => e.OwnerId == _userId)
                    .Select(e => new NoteListItem
                    {
                        Id = e.Id,
                        NoteTitle = e.NoteTitle,
                        NoteDate = e.NoteDate,
                        NoteContent = e.NoteContent,
                        TypeOfNote = e.TypeOfNote
                    });
                return query.ToArray();
            }
        }

        public NoteDetails GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.Id == id);
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

        }
        public bool UpdateNote(NoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Notes.Single(e => e.Id == model.Id);
                entity.Id = model.Id;
                entity.NoteTitle = model.NoteTitle;
                entity.NoteDate = model.NoteDate;
                entity.NoteContent = model.NoteContent;
                entity.TypeOfNote = model.TypeOfNote;
                entity.HiveId = model.HiveId;
                entity.QueenId = model.QueenId;
                entity.LocationId = model.LocationId;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteNote(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Notes.Single(e => e.Id == id);
                ctx.Notes.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
