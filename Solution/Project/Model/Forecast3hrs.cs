using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Project.Model
{
    public class Forecast3hrs : INotifyPropertyChanged
    {
        private List<WeatherCondition> _conditions;
        [JsonProperty(PropertyName = "weather")]
        public List<WeatherCondition> Conditions
        {
            get
            {
                return _conditions;
            }
            set
            {
                if (_conditions != value)
                {
                    _conditions = value;
                    OnPropertyChanged("Conditions");
                }
            }
        }

        private Rain _rain;
        [JsonProperty(PropertyName = "rain")]
        public Rain Rain
        {
            get
            {
                return _rain;
            }
            set
            {
                if (_rain != value)
                {
                    _rain = value;
                    OnPropertyChanged("Rain");
                }
            }
        }

        private WeatherParameters _parameters;
        [JsonProperty(PropertyName = "main")]
        public WeatherParameters Parameters
        {
            get
            {
                return _parameters;
            }
            set
            {
                if (_parameters != value)
                {
                    _parameters = value;
                    OnPropertyChanged("Parameters");
                }
            }
        }

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
                OnPropertyChanged("Day");
                OnPropertyChanged("DayNumber");
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
                if (_day != value)
                {
                    _day = value;
                    OnPropertyChanged("Day");
                }
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
                if (_dayNumber != value)
                {
                    _dayNumber = value;
                    OnPropertyChanged("DayNumber");
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
