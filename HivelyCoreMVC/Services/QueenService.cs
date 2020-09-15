using HivelyCoreMVC.Data;
using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.HiveModels;
using HivelyCoreMVC.Models.NoteModels;
using HivelyCoreMVC.Models.QueenModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public class QueenService
    {
        private readonly Guid _userId;
        public QueenService()
        {

        }

        public QueenService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateQueen(QueenCreate model)
        {
            var entity = new Queen()
            {
                OwnerId = _userId,
                Age = model.Age,
                Color = model.Color,
                OriginDate = model.OriginDate,
                OriginLocation = model.OriginLocation,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Queens.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<QueenListItem> GetQueens()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Queens.Where(e => e.OwnerId == _userId)
                    .Select(e => new QueenListItem
                    {
                        Age = e.Age,
                        Color = e.Color,
                        OriginDate = e.OriginDate,
                        OriginLocation = e.OriginLocation,
                        Notes = e.Notes
                    });
                return query.ToArray();
            }
        }

        public QueenDetails GetQueenById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Queens
                        .Single(e => e.Id == id);
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

        }
        public bool UpdateQueen(QueenEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Queens.Single(e => e.Id == model.Id);
                entity.Id = model.Id;
                entity.Age = model.Age;
                entity.Color = model.Color;
                entity.OriginDate = model.OriginDate;
                entity.OriginLocation = model.OriginLocation;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteQueen(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Queens.Single(e => e.Id == id);
                ctx.Queens.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
