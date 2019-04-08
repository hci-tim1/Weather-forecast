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
        private List<Forecast3hrs> _forecast3hrs;
        [JsonProperty(PropertyName = "list")]
        public List<Forecast3hrs> Forecast3hrs
        {
            get
            {
                return _forecast3hrs;
            }
            set
            {
                if (_forecast3hrs != value)
                {
                    _forecast3hrs = value;
                    OnPropertyChanged("Forecast3hrs");
                    OnPropertyChanged("MinTemperatureDay1");
                    OnPropertyChanged("MaxTemperatureDay1");
                    OnPropertyChanged("MinTemperatureDay2");
                    OnPropertyChanged("MaxTemperatureDay2");
                    OnPropertyChanged("MinTemperatureDay3");
                    OnPropertyChanged("MaxTemperatureDay3");
                    OnPropertyChanged("MinTemperatureDay4");
                    OnPropertyChanged("MaxTemperatureDay4");
                    OnPropertyChanged("MinTemperatureDay5");
                    OnPropertyChanged("MaxTemperatureDay5");
                    OnPropertyChanged("FirstDay");
                    OnPropertyChanged("SecondDay");
                    OnPropertyChanged("ThirdDay");
                    OnPropertyChanged("FourthDay");
                    OnPropertyChanged("FifthDay");
                }
            }
        }

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
            set
            {
                FirstDay = value;
            }
        }

        public Forecast3hrs SecondDay
        {
            get
            {
                return Forecast3hrs[firstDayEnd];
            }
            set
            {
                SecondDay = value;
            }
        }

        public Forecast3hrs ThirdDay
        {
            get
            {
                return Forecast3hrs[secondDayEnd];
            }
            set
            {
                ThirdDay = value;
            }
        }

        public Forecast3hrs FourthDay
        {
            get
            {
                return Forecast3hrs[thirdDayEnd];
            }
            set
            {
                FourthDay = value;
            }
        }

        public Forecast3hrs FifthDay
        {
            get
            {
                return Forecast3hrs[fourthDayEnd];
            }
            set
            {
                FifthDay = value;
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
            set
            {
                MinTemperatureDay1 = value;
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
            set
            {
                MaxTemperatureDay1 = value;
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
            set
            {
                MinTemperatureDay2 = value;
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
            set
            {
                MaxTemperatureDay2 = value;
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
            set
            {
                MinTemperatureDay3 = value;
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
            set
            {
                MaxTemperatureDay3 = value;
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
            set
            {
                MinTemperatureDay4 = value;
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
            set
            {
                MaxTemperatureDay4 = value;
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
            set
            {
                MinTemperatureDay5 = value;
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
            set
            {
                MaxTemperatureDay5 = value;
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
