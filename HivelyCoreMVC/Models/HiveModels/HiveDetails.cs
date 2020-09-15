using HivelyCoreMVC.Data.Entities;
using HivelyCoreMVC.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.HiveModels
{
    public class HiveDetails
    {
        public int Id { get; set; }
        public string HiveName { get; set; }
        public DateTime OriginDate { get; set; }
        public int NumberOfDeeps { get; set; }
        public bool HasSwarmed { get; set; }
        public Status Status { get; set; }
        public List<NoteListItem> Notes { get; set; }
        public Location Locations { get; set; }
        public string LocationName { get; set; }
        public int LocationId { get; set; }

        public Queen Queens { get; set; }
        public string QueenName { get; set; }
        public WorkerBee WorkerBees { get; set; }
    }
}
