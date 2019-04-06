using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Model
{
    public class Coordinates
    {
        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        private double Latitude1 { set { Latitude = value; } }

        [JsonProperty(PropertyName = "longitude")]
        private double Longitude1 { set { Longitude = value; } }
    }
}
