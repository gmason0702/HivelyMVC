using HivelyCoreMVC.Data.Entities;
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
    }
}
