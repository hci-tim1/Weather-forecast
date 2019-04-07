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
        public Rain Rain
        {
            get; set;
        }

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

        private string _day;
        public string Day
        {
            get
            {
                return time.DayOfWeek.ToString().Substring(0, 3);
            }
            set
            {
                _day = value;
            }
        }

        private string _dayNumber;
        public string DayNumber
        {
            get
            {
                return time.Day.ToString();
            }
            set
            {
                _dayNumber = value;
            }
        }
    }
}
