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
    /// Interaction logic for Day5View.xaml
    /// </summary>
    public partial class Day5View : Window
    {
        public Day5View()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Day 5",
                    Values = new ChartValues<double>()
                }
            };
            Labels = new List<string>();
            DataContext = this;
        }

        public void RefreshGraph(List<double> temp, List<string> hours, string day, string city)
        {
            SeriesCollection[0].Values = temp.AsChartValues();
            Labels = new List<string>();
            Title = city + ", " + day;
            foreach (string hour in hours)
            {
                Labels.Add(hour.Substring(0, 5));
            }
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
