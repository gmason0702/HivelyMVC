using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Data.Entities
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public Guid OwnerId { get; set; }
        public string LocationName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string MapLink { get; set; }

        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
        public virtual ICollection<Hive> Hives { get; set; }
    }
}

