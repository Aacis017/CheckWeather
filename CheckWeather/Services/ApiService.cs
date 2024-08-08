using CheckWeather.Models;
using Newtonsoft.Json;

namespace CheckWeather.Services
{
    public static class ApiService
    {
        public static async Task<Root> GetCurrentWeather(double latitude, double longitude)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(10);

                try
                {
                    var response = await httpClient.GetStringAsync(string.Format("https://api.weatherapi.com/v1/current.json?key=e80fcaf9e84a406c8b965100240408&q={0},{1}&aqi=no", latitude, longitude));
                    return JsonConvert.DeserializeObject<Root>(response);
                }
                catch (HttpRequestException ex)
                {
                    return null;
                }
                catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
                {
                    return null;
                }
            }

        }
    }
   
}
