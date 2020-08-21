using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.HiveModels;
using HivelyCoreMVC.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.QueenModels
{
    public class QueenDetails
    {
        public int Id { get; set; }
        public Guid OwnerId { get; set; }
        public int Age { get; set; }
        public Color Color { get; set; }
        public DateTime OriginDate { get; set; }
        public string OriginLocation { get; set; }

        public List<NoteListItem> Notes { get; set; }
        public List<HiveListItem> Hives { get; set; }
    }
}
