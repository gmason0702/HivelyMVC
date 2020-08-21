using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Data.Entities
{
    public enum NoteType { FieldNote, Reminder, Location, Workers, Queen, Hive }

    public class Note
    {
        [Key]
        public int Id { get; set; }
        public Guid OwnerId { get; set; }
        public string NoteTitle { get; set; }
        public DateTime NoteDate { get; set; }
        public string NoteContent { get; set; }
        public NoteType TypeOfNote { get; set; }


        [ForeignKey(nameof(Hive))]
        public int? HiveId { get; set; }
        public virtual Hive Hive { get; set; }

        public virtual Queen Queen { get; set; }
        public int? QueenId { get; set; }
        public virtual Location Location { get; set; }
        public int? LocationId { get; set; }
    }
}
