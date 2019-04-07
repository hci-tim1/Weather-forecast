using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Project.Model
{
    public class Forecast5d : INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "list")]
        public List<Forecast3hrs> Forecast3hrs { get; set; }

        private City _city;
        [JsonProperty(PropertyName = "city")]
        public City City
        {
            get
            {
                return _city;
            }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        public int firstDayEnd;
        private int secondDayEnd;
        private int thirdDayEnd;
        private int fourthDayEnd;
        
        public Forecast3hrs FirstDay
        {
            get
            {
                return Forecast3hrs[0];
            }
        }

        public Forecast3hrs SecondDay
        {
            get
            {
                return Forecast3hrs[firstDayEnd];
            }
        }

        public Forecast3hrs ThirdDay
        {
            get
            {
                return Forecast3hrs[secondDayEnd];
            }
        }

        public Forecast3hrs FourthDay
        {
            get
            {
                return Forecast3hrs[thirdDayEnd];
            }
        }

        public Forecast3hrs FifthDay
        {
            get
            {
                return Forecast3hrs[fourthDayEnd];
            }
        }

        public double MinTemperatureDay1
        {
            get
            {
                int day = Forecast3hrs[0].Time.Day;
                firstDayEnd = Forecast3hrs.Where(f => f.Time.Day == day).Count();
                List<Forecast3hrs> sameDay = Forecast3hrs.Where(f => f.Time.Day == day).ToList();
                return sameDay.Min(f => f.Parameters.MinTemperature);
            }
        }

        public double MaxTemperatureDay1
        {
            get
            {
                int day = Forecast3hrs[0].Time.Day;
                List<Forecast3hrs> sameDay = Forecast3hrs.Where(f => f.Time.Day == day).ToList();
                return sameDay.Max(f => f.Parameters.MaxTemperature);
            }
        }

        public double MinTemperatureDay2
        {
            get
            {
                int day = Forecast3hrs[firstDayEnd].Time.Day;
                secondDayEnd = firstDayEnd + Forecast3hrs.Where(f => f.Time.Day == day).Count();
                List<Forecast3hrs> sameDay = Forecast3hrs.Where(f => f.Time.Day == day).ToList();
                return sameDay.Min(f => f.Parameters.MinTemperature);
            }
        }

        public double MaxTemperatureDay2
        {
            get
            {
                int day = Forecast3hrs[firstDayEnd].Time.Day;
                List<Forecast3hrs> sameDay = Forecast3hrs.Where(f => f.Time.Day == day).ToList();
                return sameDay.Max(f => f.Parameters.MaxTemperature);
            }
        }

        public double MinTemperatureDay3
        {
            get
            {
                int day = Forecast3hrs[secondDayEnd].Time.Day;
                thirdDayEnd = secondDayEnd + Forecast3hrs.Where(f => f.Time.Day == day).Count();
                List<Forecast3hrs> sameDay = Forecast3hrs.Where(f => f.Time.Day == day).ToList();
                return sameDay.Min(f => f.Parameters.MinTemperature);
            }
        }

        public double MaxTemperatureDay3
        {
            get
            {
                int day = Forecast3hrs[secondDayEnd].Time.Day;
                List<Forecast3hrs> sameDay = Forecast3hrs.Where(f => f.Time.Day == day).ToList();
                return sameDay.Max(f => f.Parameters.MaxTemperature);
            }
        }

        public double MinTemperatureDay4
        {
            get
            {
                int day = Forecast3hrs[thirdDayEnd].Time.Day;
                fourthDayEnd = thirdDayEnd + Forecast3hrs.Where(f => f.Time.Day == day).Count();
                List<Forecast3hrs> sameDay = Forecast3hrs.Where(f => f.Time.Day == day).ToList();
                return sameDay.Min(f => f.Parameters.MinTemperature);
            }
        }

        public double MaxTemperatureDay4
        {
            get
            {
                int day = Forecast3hrs[thirdDayEnd].Time.Day;
                List<Forecast3hrs> sameDay = Forecast3hrs.Where(f => f.Time.Day == day).ToList();
                return sameDay.Max(f => f.Parameters.MaxTemperature);
            }
        }

        public double MinTemperatureDay5
        {
            get
            {
                int day = Forecast3hrs[fourthDayEnd].Time.Day;
                List<Forecast3hrs> sameDay = Forecast3hrs.Where(f => f.Time.Day == day).ToList();
                return sameDay.Min(f => f.Parameters.MinTemperature);
            }
        }

        public double MaxTemperatureDay5
        {
            get
            {
                int day = Forecast3hrs[fourthDayEnd].Time.Day;
                List<Forecast3hrs> sameDay = Forecast3hrs.Where(f => f.Time.Day == day).ToList();
                return sameDay.Max(f => f.Parameters.MaxTemperature);
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
