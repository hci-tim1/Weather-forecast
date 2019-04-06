using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Model
{
    public class Forecast3hrs
    {
        [JsonProperty(PropertyName = "weather")]
        public List<WeatherCondition> Conditions { get; set; }

        [JsonProperty(PropertyName = "rain")]
        public Rain Rain { get; set; }

        [JsonProperty(PropertyName = "main")]
        public WeatherParameters Parameters { get; set; }

        private DateTime time;
        [JsonProperty(PropertyName = "dt_txt")]
        public DateTime Time
        {
            get
            {
                return DateTime.Parse(time.ToString("dd/MM/yyyy HH:mm"));
            }

            set
            {
                time = value;
            }
        }
    }
}
