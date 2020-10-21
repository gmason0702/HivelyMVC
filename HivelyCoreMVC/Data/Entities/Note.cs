using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Data.Entities
{

    public enum NoteType { FieldNote, Reminder, Location, Workers, Queen, Hive }
    public enum Rating
    {
        [Display(Name =" ")]
        NoRating, 
        Excellent = 5,
        [Display(Name = "Above Average")]
        AboveAverage =4,
        Average = 3,
        [Display(Name = "Below Average")]
        BelowAverage = 2,
        Poor =1
    }
    public enum Job
    {
        [Display(Name = " ")]
        Blank, 
        [Display(Name = "Protein Patties")]
        ProteinPatties,
        [Display(Name = "Varroa Check")]
        VarroaTest,
        [Display(Name = "Varroa Treatment")]
        VarroaTreatment,
        [Display(Name = "Remove Varroa Treatment")]
        RemoveVarroaTreatment,
        [Display(Name = "Swarm Check")]
        SwarmCheck, 
        [Display(Name ="Catch a Swarm")]
        CatchSwarm, 
        [Display(Name ="Hive Check")]
        HiveCheck,
        [Display(Name = "Add Package Bees")]
        AddPackageBees,
        [Display(Name = "Add Honey Super")]
        AddSuper,
        [Display(Name ="Add Sugar Water")]
        SugarWater,
        [Display(Name ="Place Queen Excluder")]
        PlaceQueenExcluder,
        [Display(Name ="Harvest Honey")]
        HarvestHoney,
        [Display(Name ="Install Mouse Guard")]
        MouseGuard,
        [Display(Name ="Add Sugar Candy")]
        SugarCandy
    }

    public class Note
    {
        [Key]
        public int Id { get; set; }
        public Guid OwnerId { get; set; }
        public string NoteTitle { get; set; }
        public DateTime NoteDate { get; set; }
        public string NoteContent { get; set; }
        public NoteType TypeOfNote { get; set; }
        public Rating HiveRating { get; set; }
        public Job JobType { get; set; }
        public string? FilePath { get; set; }

        [ForeignKey(nameof(Hive))]
        public int? HiveId { get; set; }
        public virtual Hive Hive { get; set; }

        public virtual Queen Queen { get; set; }
        public int? QueenId { get; set; }
        public virtual Location Location { get; set; }
        public int? LocationId { get; set; }
    }
}
