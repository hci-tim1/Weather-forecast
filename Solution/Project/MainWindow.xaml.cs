using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Project.ApiService;
using Project.Model;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Forecast5d _forecast;
        public Forecast5d Forecast
        {
            get
            {
                return _forecast;
            }
            set
            {
                if (value != _forecast)
                {
                    _forecast = value;
                    OnPropertyChanged("Forecast");
                }
            }
        }
        public int CurrentCityId { get; set; }
        public Coordinates CurrentCityCoords { get; set; }
        public Thread DataFetchThread { get; set; }

        private Cities _cities;
        public Cities Cities
        {
            get
            {
                return _cities;
            }
            set
            {
                if (value != _cities)
                {
                    _cities = value;
                    OnPropertyChanged("Cities");
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

        public void StartThread(bool coordinates)
        {
            if(DataFetchThread != null)
            {
                DataFetchThread.Abort();
            }

            DataFetchThread = new Thread(new ParameterizedThreadStart(DoWork))
            {
                IsBackground = true
            };

            if (coordinates)
            {
                DataFetchThread.Start(CurrentCityCoords);
            }
            else
            {
                DataFetchThread.Start(CurrentCityId);
            }
        }

        private void DoWork(object o)
        {
            if(o is int)
            {
                RunWithID((int)o);
            }
            else
            {
                RunWithCoords((Coordinates)o);
            }
        }

        private void RunWithID(int ID)
        {
            while (true)
            {
                Forecast = WeatherApi.RunAsync(ID).GetAwaiter().GetResult();
                Console.WriteLine(Forecast.City.Name);
                Thread.Sleep(10 * 60 * 1000); // gets fresh data every 10mins
            }
        }

        private void RunWithCoords(Coordinates coords)
        {
            while (true)
            {
                Forecast = WeatherApi.RunAsync(coords).GetAwaiter().GetResult();
                Console.WriteLine(Forecast.City.Name);
                Thread.Sleep(10 * 60 * 1000); // gets fresh data every 10mins
            }
        }

        private void ReadAllCities()
        {
            string path = "C:\\Users\\Danijel\\Documents\\GitHub\\Weather-forecast\\Solution\\Project\\Data\\cities.json";
            // ko zna kako se namesta relative path u odnosu na projekat, nek izvoli
            using(StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                Cities = JsonConvert.DeserializeObject<Cities>(json);
            }
        }

        public MainWindow()
        {
            ReadAllCities();
            CurrentCityCoords = WeatherApi.GetGeolocation().GetAwaiter().GetResult();
            CurrentCityId = 524901; // presenter should set city id or coordinates, and call start thread 
            DataContext = this;
            InitializeComponent();
            StartThread(true);
        }
    }
}
