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
    }
}
