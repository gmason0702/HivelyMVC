using HivelyCoreMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.HiveModels
{
    public class HiveEdit
    {
        public int Id { get; set; }
        public string HiveName { get; set; }
        public DateTime OriginDate { get; set; }
        public int NumberOfDeeps { get; set; }
        public bool HasSwarmed { get; set; }
        public Status Status { get; set; }
        public int LocationId { get; set; }
    }
}
