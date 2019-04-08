using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace Project.Model
{
    public class WeatherParameters : INotifyPropertyChanged
    {
        private double _currentTemperature;
        [JsonProperty(PropertyName = "temp")]
        public double CurrentTemperature
        {
            get
            {
                return _currentTemperature;
            }
            set
            {
                if (_currentTemperature != value)
                {
                    _currentTemperature = value;
                    OnPropertyChanged("CurrentTemperature");
                }
            }
        }

        private double _minTemperature;
        [JsonProperty(PropertyName = "temp_min")]
        public double MinTemperature
        {
            get
            {
                return _minTemperature;
            }
            set
            {
                if (_minTemperature != value)
                {
                    _minTemperature = value;
                    OnPropertyChanged("MinTemperature");
                }
            }
        }

        private double _maxTemperature;
        [JsonProperty(PropertyName = "temp_max")]
        public double MaxTemperature
        {
            get
            {
                return _maxTemperature;
            }
            set
            {
                if (_maxTemperature != value)
                {
                    _maxTemperature = value;
                    OnPropertyChanged("MaxTemperature");
                }
            }
        }

        private int _humidity;
        [JsonProperty(PropertyName = "humidity")]
        public int Humidity
        {
            get
            {
                return _humidity;
            }
            set
            {
                if (_humidity != value)
                {
                    _humidity = value;
                    OnPropertyChanged("Humidity");
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
