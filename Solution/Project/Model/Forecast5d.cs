using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Model
{
    public class Forecast5d
    {
        [JsonProperty(PropertyName = "list")]
        public List<Forecast3hrs> Forecast3hrs { get; set; }

        [JsonProperty(PropertyName = "city")]
        public City City { get; set; }
    }
}
