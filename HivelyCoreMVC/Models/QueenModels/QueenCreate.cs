using HivelyCoreMVC.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.QueenModels
{
    public class QueenCreate
    {
        public int Id { get; set; }
        public Guid OwnerId { get; set; }
        public int Age { get; set; }
        public Color Color { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Origin Date")]
        [DataType(DataType.Date, ErrorMessage = ("Please enter a valid date"))]
        [UIHint("ADateTime")]
        public DateTime OriginDate { get; set; }

        [DisplayName("Origin Location")]
        public string OriginLocation { get; set; }
        public int HiveId { get; set; }
    }
}
