using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
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
using System.Windows.Shapes;

namespace Project.DetailedViews
{
    /// <summary>
    /// Interaction logic for Day3View.xaml
    /// </summary>
    public partial class Day3View : Window
    {
        public Day3View(List<double> temp, List<string> hours, string day, string city)
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = day,
                    Values = temp.AsChartValues()
                }
            };
            Title = city + ", " + day;
            Labels = new List<string>();
            foreach (string hour in hours)
            {
                Labels.Add(hour.Substring(0, 5));
            }

            DataContext = this;
        }

        public Day3View()
        {
            InitializeComponent();
        }
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
