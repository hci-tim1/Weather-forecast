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
using Project.DetailedViews;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Day1View day1View = new Day1View();
        private Day2View day2View = new Day2View();
        private Day3View day3View = new Day3View();
        private Day4View day4View = new Day4View();
        private Day5View day5View = new Day5View();

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
                    OnPropertyChanged("FavoritesIcon");
                    OnPropertyChanged("FavoritesTooltip");
                    var temp = Forecast.GetFirstDayTemp();
                    var hours = Forecast.GetFirstDayHours();
                    this.Dispatcher.Invoke(() =>
                    {
                        day1View.RefreshGraph(temp, hours, 
                            Forecast.FirstDay.Day + " " + Forecast.FirstDay.DayNumber, Forecast.City.Name);
                    });
                    temp = Forecast.GetSecondDayTemp();
                    hours = Forecast.GetSecondDayHours();
                    this.Dispatcher.Invoke(() =>
                    {
                        day2View.RefreshGraph(temp, hours,
                            Forecast.SecondDay.Day + " " + Forecast.SecondDay.DayNumber, Forecast.City.Name);
                    });
                    temp = Forecast.GetThirdDayTemp();
                    hours = Forecast.GetThirdDayHours();
                    this.Dispatcher.Invoke(() =>
                    {
                        day3View.RefreshGraph(temp, hours,
                            Forecast.ThirdDay.Day + " " + Forecast.ThirdDay.DayNumber, Forecast.City.Name);
                    });
                    temp = Forecast.GetFourthDayTemp();
                    hours = Forecast.GetFourthDayHours();
                    this.Dispatcher.Invoke(() =>
                    {
                        day4View.RefreshGraph(temp, hours,
                            Forecast.FourthDay.Day + " " + Forecast.FourthDay.DayNumber, Forecast.City.Name);
                    });
                    temp = Forecast.GetFifthDayTemp();
                    hours = Forecast.GetFifthDayHours();
                    this.Dispatcher.Invoke(() =>
                    {
                        day5View.RefreshGraph(temp, hours,
                            Forecast.FifthDay.Day + " " + Forecast.FifthDay.DayNumber, Forecast.City.Name);
                    });
                }
            }
        }
        public int CurrentCityId { get; set; }
        public Coordinates CurrentCityCoords { get; set; }
        public Thread DataFetchThread { get; set; }

        private string _favoritesIcon;
        public string FavoritesIcon
        {
            get
            {
                try
                {
                    if (Favorites.Favorites.Contains(Forecast.City.Name + "," + Forecast.City.Country))
                        return "Data/icons/fullstar_icon.png";
                    else
                        return "Data/icons/emptystar_icon.png";
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                _favoritesIcon = value;
            }
        }

        private string _favoritesTooltip;
        public string FavoritesTooltip
        {
            get
            {
                try
                {
                    if (Favorites.Favorites.Contains(Forecast.City.Name + "," + Forecast.City.Country))
                        return "Remove from favorites";
                    else
                        return "Add to favorites";
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                _favoritesTooltip = value;
            }
        }

        private string _refreshDate;
        public string RefreshDate
        {
            get
            {
                return _refreshDate;
            }
            set
            {
                if (value != _refreshDate)
                {
                    _refreshDate = value;
                    OnPropertyChanged("RefreshDate");
                }
            }
        }

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

        private Cities _favorites;
        public Cities Favorites
        {
            get
            {
                return _favorites;
            }
            set
            {
                if (value != _favorites)
                {
                    _favorites = value;
                    OnPropertyChanged("Favorites");
                }
            }
        }

        private List<string> _history;
        public List<string> History
        {
            get
            {
                return _history;
            }
            set
            {
                if (_history != value)
                {
                    _history = value;
                    OnPropertyChanged("History");
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
                IsBackground = true,
                ApartmentState = ApartmentState.STA
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
                if (History == null)
                {
                    History = new List<string>();
                }
                Forecast = WeatherApi.RunAsync(ID).GetAwaiter().GetResult();
                CurrentCityId = Forecast.City.ID;
                if (!(History.Contains(Forecast.City.Name + "," + Forecast.City.Country)))
                {
                    History.Add(Forecast.City.Name + "," + Forecast.City.Country);
                    History = new List<string>(History);
                }
                RefreshDate = DateTime.Now.ToString(@"HH\:mm\:ss");
                Thread.Sleep(10 * 60 * 1000); // gets fresh data every 10mins
            }
        }

        private void RunWithCoords(Coordinates coords)
        {
            while (true)
            {
                if (History == null)
                {
                    History = new List<string>();
                }
                Forecast = WeatherApi.RunAsync(coords).GetAwaiter().GetResult();
                CurrentCityId = Forecast.City.ID;
                if (!(History.Contains(Forecast.City.Name + "," + Forecast.City.Country)))
                {
                    History.Add(Forecast.City.Name + "," + Forecast.City.Country);
                    History = new List<string>(History);
                }
                RefreshDate = DateTime.Now.ToString(@"HH\:mm\:ss");
                Thread.Sleep(10 * 60 * 1000); // gets fresh data every 10mins
            }
        }

        private void ReadAllCities()
        {
            string path = "../../Data/cities.json";
            // ko zna kako se namesta relative path u odnosu na projekat, nek izvoli
            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                Cities = JsonConvert.DeserializeObject<Cities>(json);
            }
        }

        private void ReadFavorites()
        {
            string path = "../../Data/favorites.json";
            // ko zna kako se namesta relative path u odnosu na projekat, nek izvoli
            using (StreamReader sr = new StreamReader(path))
            {
                string favorites = sr.ReadToEnd();
                Favorites = JsonConvert.DeserializeObject<Cities>(favorites);
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                day1View.Hide();
                day2View.Hide();
                day3View.Hide();
                day4View.Hide();
                day5View.Hide();

                day1View.Show();
            });
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                day1View.Hide();
                day2View.Hide();
                day3View.Hide();
                day4View.Hide();
                day5View.Hide();

                day2View.Show();
            });        
        }

        private void Grid_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                day1View.Hide();
                day2View.Hide();
                day3View.Hide();
                day4View.Hide();
                day5View.Hide();

                day3View.Show();
            }); 
        }

        private void Grid_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                day1View.Hide();
                day2View.Hide();
                day3View.Hide();
                day4View.Hide();
                day5View.Hide();

                day4View.Show();
            });
        }

        private void Grid_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                day1View.Hide();
                day2View.Hide();
                day3View.Hide();
                day4View.Hide();
                day5View.Hide();

                day5View.Show();
            });  
        }

        private void Refresh(object sender, MouseButtonEventArgs e)
        {
            StartThread(false);
        }

        private void FavoritesHistory_Click(object sender, RoutedEventArgs e)
        {
            MenuItem m = (MenuItem)e.OriginalSource;
            try
            {
                string data = (string)m.DataContext;
                int idx = data.LastIndexOf(',');
                string[] city = { data.Substring(0, idx), data.Substring(idx + 1) };
                ShowCity(city);
            }
            catch
            {
                return;
            }
        }

        private void ShowCity(string[] city)
        {
            CurrentCityId = Cities.AllCities.Where(c => (c.Name == city[0] && c.Country == city[1])).Select(c => c.ID).First();
            StartThread(false);
        }

        private void Favorites_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Favorites.Favorites.Contains(Forecast.City.Name + "," + Forecast.City.Country))
                Favorites.Favorites.Remove(Forecast.City.Name + "," + Forecast.City.Country);
            else
                Favorites.Favorites.Add(Forecast.City.Name + "," + Forecast.City.Country);
            OnPropertyChanged("FavoritesIcon");
            OnPropertyChanged("FavoritesTooltip");
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            string path = "../../Data/favorites.json";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(JsonConvert.SerializeObject(Favorites));
            }
            Environment.Exit(0);
        }

        private void SearchTxtBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox t = (TextBox)e.OriginalSource;
                try
                {
                    string data = (string)t.Text;
                    int idx = data.LastIndexOf(',');
                    string[] city = { data.Substring(0, idx), data.Substring(idx + 1) };
                    ShowCity(city);
                }
                catch
                {
                    return;
                }
            }
        }

        public MainWindow()
        {
            ShowWindow(GetConsoleWindow(), SW_HIDE);
            InitializeComponent();
            ReadAllCities();
            ReadFavorites();
            CurrentCityCoords = WeatherApi.GetGeolocation().GetAwaiter().GetResult();
            CurrentCityId = 524901; // presenter should set city id or coordinates, and call start thread
            DataContext = this;
            StartThread(true);
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
    }
}
