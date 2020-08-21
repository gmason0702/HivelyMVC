using HivelyCoreMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.QueenModels
{
    public class QueenEdit
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public Color Color { get; set; }
        public DateTime OriginDate { get; set; }
        public string OriginLocation { get; set; }
    }
}
