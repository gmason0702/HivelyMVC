using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Data.Entities
{
    public enum Status { Winter, SpringBrood, VarroaTreatment, DiseaseCheck, FeedCandy, FeedWater, Swarm, Flow, FallPrep }

    public class Hive
    {

        [Key]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string HiveName { get; set; }
        public DateTime OriginDate { get; set; }
        public int NumberOfDeeps { get; set; }
        public bool HasSwarmed { get; set; }
        public Status Status { get; set; }


        //[ForeignKey(nameof(WorkerBees))]
        //public int WorkersId { get; set; }
        public virtual ICollection<WorkerBee> WorkerBees { get; set; }


        [ForeignKey(nameof(Queens))]
        public int QueenId { get; set; }
        public virtual Queen Queens { get; set; }


        [ForeignKey(nameof(Locations))]
        public int LocationId { get; set; }
        public virtual Location Locations { get; set; }


        //[ForeignKey(nameof(Notes))]
        //public int NoteId { get; set; }
        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
