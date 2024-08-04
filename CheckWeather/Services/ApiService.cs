using CheckWeather.Models;
using Newtonsoft.Json;

namespace CheckWeather.Services
{
    public static class ApiService
    {
        public static async Task<Root> GetCurrentWeather(double lattitude, double longitude)
        {
            var httpClient = new HttpClient(); //takes request to server

            var response = await httpClient.GetStringAsync(string.Format("https://api.weatherapi.com/v1/current.json?key=e80fcaf9e84a406c8b965100240408&q={0},{1}&aqi=no", lattitude, longitude));
           
            return JsonConvert.DeserializeObject<Root>(response);

        }
    }
   
}
