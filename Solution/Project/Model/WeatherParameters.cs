using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Model
{
    public class WeatherParameters
    {
        [JsonProperty(PropertyName = "temp")]
        public double CurrentTemperature { get; set; }

        [JsonProperty(PropertyName = "temp_min")]
        public double MinTemperature { get; set; }

        [JsonProperty(PropertyName = "temp_max")]
        public double MaxTemperature { get; set; }

        [JsonProperty(PropertyName = "humidity")]
        public int Humidity { get; set; }

        [JsonProperty(PropertyName = "weather")]
        public WeatherCondition Condition { get; set; }

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
