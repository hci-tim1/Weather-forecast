using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Project.Model;
using System.Net.Http.Headers;
using System.Web;

namespace Project.ApiService
{
    public static class WeatherApi
    {
        private const string apiKey = "f95dd31b237b29268f6dcf20ee40f033";
        private const string baseURL = "http://api.openweathermap.org/data/2.5/forecast?";
        private const string geoURL = "http://geoip-db.com/json/";
        static HttpClient client = new HttpClient();

        public static async Task<Coordinates> GetGeolocation()
        {
            Coordinates coordinates = null;

            HttpResponseMessage response = client.GetAsync(geoURL).Result;

            if (response.IsSuccessStatusCode)
            {
                coordinates = await response.Content.ReadAsAsync<Coordinates>();// convert from GZip
            }
            return coordinates;
        }

        static async Task<Forecast5d> GetForecast5dAsync(string path)
        {
            Forecast5d forecast = null;
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                forecast = await response.Content.ReadAsAsync<Forecast5d>();
            }
            return forecast;
        }

        public static async Task<Forecast5d> RunAsync(int ID)
        {
            string query = baseURL;
            var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("id", ID.ToString()),
                new KeyValuePair<string, string>("APPID", apiKey),
                new KeyValuePair<string, string>("units", "metric")
            });
            query += content.ReadAsStringAsync().Result;
            
            Forecast5d forecast = null;
            try
            {
                forecast = await GetForecast5dAsync(query);
                return forecast;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static async Task<Forecast5d> RunAsync(Coordinates coord)
        {
            string query = baseURL;
            var content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]{
                new KeyValuePair<string, string>("lat", coord.Latitude.ToString()),
                new KeyValuePair<string, string>("lon", coord.Longitude.ToString()),
                new KeyValuePair<string, string>("APPID", apiKey),
                new KeyValuePair<string, string>("units", "metric")
            });
            query += content.ReadAsStringAsync().Result;

            Forecast5d forecast = null;
            try
            {
                forecast = await GetForecast5dAsync(query);
                return forecast;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
