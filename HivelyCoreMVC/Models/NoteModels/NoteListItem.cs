using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.HiveModels;
using HivelyCoreMVC.Models.LocationModels;
using HivelyCoreMVC.Models.QueenModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.NoteModels
{
    public class NoteListItem
    {
        public int Id { get; set; }
        public Guid OwnerId { get; set; }
        public string NoteTitle { get; set; }
        public DateTime NoteDate { get; set; }
        public string NoteContent { get; set; }
        public NoteType TypeOfNote { get; set; }
        public ICollection<Hive> Hives { get; set; }
        public ICollection<Queen> Queens { get; set; }
        public ICollection<Location> Locations { get; set; }
        public List<IFormFile> File { get; set; } = new List<IFormFile>();


    }
}
