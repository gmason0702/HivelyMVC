using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Data.Entities
{
    public enum Color
    {
        [Description("Year ending in 1 or 6")] White,
        [Description("Year ending in 2 or 7")] Yellow,
        [Description("Year ending in 3 or 8")] Red,
        [Description("Year ending in 4 or 9")] Green,
        [Description("Year ending in 5 or 0")] Blue
    }
    public class Queen
    {
        [Key]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string QueenName { get; set; }
        public int Age { get; set; }
        public Color Color { get; set; }
        public DateTime OriginDate { get; set; }
        public string OriginLocation { get; set; }


        public virtual ICollection<Hive> Hives { get; set; } = new List<Hive>();
        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
