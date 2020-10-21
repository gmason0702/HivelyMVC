using HivelyCoreMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.HiveModels
{
    //public enum Status { Winter, SpringBrood, VarroaTreatment, DiseaseCheck, FeedCandy, FeedWater, Swarm, Flow, FallPrep }

    public class HiveListItem
    {
        public int Id { get; set; }
        public Guid OwnerId { get; set; }
        [DisplayName("Name")]
        public string HiveName { get; set; }
        [DisplayName("Origin Date")]
        public DateTime OriginDate { get; set; }
        [DisplayName("# Deeps")]
        public int NumberOfDeeps { get; set; }
        [DisplayName("Swarmed?")]
        public bool HasSwarmed { get; set; }
        public Status Status { get; set; }
        public Location Location { get; set; }
        public Queen Queen { get; set; }
    }
}
