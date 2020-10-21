using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.HiveModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.NoteModels
{
    public class NoteCreate
    {
        public Guid OwnerId { get; set; }
        public string NoteTitle { get; set; }
        public DateTime NoteDate { get; set; }
        public string NoteContent { get; set; }
        public NoteType TypeOfNote { get; set; }
        public IFormFile? File { get; set; }
        public Rating HiveRating { get; set; }
        public Job JobType { get; set; }
        public List<HiveListItem> Hives { get; set; } = new List<HiveListItem>();
    }
}
