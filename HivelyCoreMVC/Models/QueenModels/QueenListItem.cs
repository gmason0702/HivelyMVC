using HivelyCoreMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.QueenModels
{
    public class QueenListItem
    {
        public int Id { get; set; }
        public Guid OwnerId { get; set; }
        public string QueenName { get; set; }
        public int Age { get; set; }
        public Color Color { get; set; }
        public DateTime OriginDate { get; set; }
        public string OriginLocation { get; set; }
        public string LocationName { get; set; }
        public ICollection<Note> Notes { get; set; }
        public int HiveId { get; set; }
        public Hive Hive { get; set; }
    }
}
