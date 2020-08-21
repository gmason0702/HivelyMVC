using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.WorkerBeeModels
{
    public class WorkerBeeListItem
    {
        public DateTime OriginDate { get; set; }
        public string OriginLocation { get; set; }
        public Hive Hive { get; set; }
    }
}
