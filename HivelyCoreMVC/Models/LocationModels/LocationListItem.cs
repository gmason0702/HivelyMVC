﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.LocationModels
{
    public class LocationListItem
    {
        public string Id { get; set; }

        [DisplayName("Location")]
        public string LocationName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [DisplayName("Long")]
        public string Longitude { get; set; }
        [DisplayName("Lat")]
        public string Latitude { get; set; }
        [DisplayName("Map")]
        public string MapLink { get; set; }
    }
}
