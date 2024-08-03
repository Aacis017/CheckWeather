using CheckWeather.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckWeather.Services
{
    public class ApiService
    {
        public async Task<Root> GetWeathers(double lattitude, double longitude)
        {
            var httpClient = new HttpClient(); //takes request to server
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=d656e853ac72e163823e7fbce2587cb7", lattitude, longitude));
            return JsonConvert.DeserializeObject<Root>(response);

        }
    }
   
}
