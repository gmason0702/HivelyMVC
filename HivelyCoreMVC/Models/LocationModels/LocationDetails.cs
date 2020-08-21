using HivelyCoreMVC.Models.HiveModels;
using HivelyCoreMVC.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.LocationModels
{
    public class LocationDetails
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string MapLink { get; set; }
        public List<NoteListItem> Notes { get; set; }
        public List<HiveListItem> Hives { get; set; }
    }
}
