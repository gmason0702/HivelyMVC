using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.WorkerBeeModels
{
    public class WorkerBeeEdit
    {
        public int Id { get; set; }
        public DateTime OriginDate { get; set; }
        public string OriginLocation { get; set; }
    }
}
