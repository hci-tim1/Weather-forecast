using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Model
{
    public class Rain
    {
        [JsonProperty(PropertyName = "3h")]
        public double Quantity { get; set; }
    }
}
