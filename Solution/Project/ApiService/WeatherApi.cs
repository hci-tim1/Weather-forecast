using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Project.ApiService
{
    public static class WeatherApi
    {
        private const string apiKey = "f95dd31b237b29268f6dcf20ee40f033";

        static HttpClient client = new HttpClient();
    }
}
