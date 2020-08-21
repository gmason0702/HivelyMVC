using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.LocationModels
{
    public class LocationCreate
    {
        public string LocationName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
