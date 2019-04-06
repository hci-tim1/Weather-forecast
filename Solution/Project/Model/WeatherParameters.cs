using Newtonsoft.Json;
using System;

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
    }
}
