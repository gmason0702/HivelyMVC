using HivelyCoreMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.WorkerBeeModels
{
    public class WorkerBeeDetails
    {
        public int Id { get; set; }
        public DateTime OriginDate { get; set; }
        public string OriginLocation { get; set; }
        public Hive Hive { get; set; }
    }
}
