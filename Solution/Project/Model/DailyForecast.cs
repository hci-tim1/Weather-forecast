using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Model
{
    public class DailyForecast
    {
        [JsonProperty(PropertyName = "dt_txt")]
        public DateTime date;
    }
}
