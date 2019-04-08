using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Project.Model
{
    public class Cities : INotifyPropertyChanged
    {
        [JsonProperty(PropertyName = "cities")]
        public ObservableCollection<City> AllCities { get; set; }

        private IEnumerable<string> _citiesNames;
        [JsonIgnore]
        public IEnumerable<string> CitiesNames
        {
            get
            {
                _citiesNames = AllCities.Select(c => c.Name + "," + c.Country);
                return _citiesNames;
            }
            set
            {
                _citiesNames = value;
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

        [JsonProperty(PropertyName = "favorites")]
        public ObservableCollection<string> Favorites
        {
            get; set;
        }
    }
}
