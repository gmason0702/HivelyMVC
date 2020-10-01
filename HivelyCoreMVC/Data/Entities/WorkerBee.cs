using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Data.Entities
{
    public class WorkerBee
    {
        [Key]
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public DateTime OriginDate { get; set; }
        public string OriginLocation { get; set; }

        [ForeignKey(nameof(Hive))]
        public int HiveId { get; set; }
        public virtual Hive Hive { get; set; } = new Hive();
    }
}
