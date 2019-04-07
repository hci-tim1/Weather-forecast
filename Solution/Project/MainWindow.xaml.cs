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

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Forecast5d Forecast { get; set; }
        public int CurrentCityId { get; set; }
        public Coordinates CurrentCityCoords { get; set; }
        public Thread DataFetchThread { get; set; }
        public Cities Cities { get; set; }

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
                Console.WriteLine(Forecast);
                Thread.Sleep(10 * 60 * 1000); // gets fresh data every 10mins
            }
        }

        private void RunWithCoords(Coordinates coords)
        {
            while (true)
            {
                Forecast = WeatherApi.RunAsync(coords).GetAwaiter().GetResult();
                Console.WriteLine(Forecast);
                Thread.Sleep(10 * 60 * 1000); // gets fresh data every 10mins
            }
        }

        private void ReadAllCities()
        {
            string path = "D:\\Fakultet\\III godina\\II semestar\\Interakcija covek racunar\\Weather-forecast\\Solution\\Project\\Data\\cities.json";
            // ko zna kako se namesta relative path u odnosu na projekat, nek izvoli
            using(StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                Cities = JsonConvert.DeserializeObject<Cities>(json);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            ReadAllCities();
            CurrentCityCoords = WeatherApi.GetGeolocation().GetAwaiter().GetResult();
            CurrentCityId = 524901; // presenter should set city id or coordinates, and call start thread
            StartThread(true);
        }
    }
}
