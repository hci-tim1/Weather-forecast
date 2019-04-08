using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Project.Model
{
    public class WeatherCondition : INotifyPropertyChanged
    {
        private string _description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string _iconID;
        [JsonProperty(PropertyName = "icon")]
        public string IconID
        {
            get
            {
                return _iconID;
            }
            set
            {
                if (_iconID != value)
                {
                    _iconID = value;
                    Icon = "Data/Icons/" + _iconID + ".png";
                    OnPropertyChanged("IconID");
                }
            }
        }

        private string _icon;
        public string Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    OnPropertyChanged("Icon");
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
