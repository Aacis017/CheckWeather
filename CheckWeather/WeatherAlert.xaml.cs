using Microsoft.Maui.Controls;
using System;

namespace CheckWeather
{
    public partial class WeatherAlert : ContentPage
    {
        private Random _random;
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(5); // Update every 5 seconds

        public WeatherAlert()
        {
            InitializeComponent();
            _random = new Random();
            StartUpdatingWeatherData();
        }

        private async void StartUpdatingWeatherData()
        {
            while (true)
            {
                GenerateRandomWeatherData();
                await Task.Delay(_updateInterval);
            }
        }

        private void GenerateRandomWeatherData()
        {
            double windSpeed = _random.NextDouble() * 20; 
            double precipitation = _random.NextDouble() * 20; 

            LblWind.Text = $"{windSpeed:F1} m/s";
            LblRain.Text = $"{precipitation:F1} mm/hr";

            bool isWindSpeedHigh = windSpeed > 8;
            bool isPrecipitationHigh = precipitation > 10;

            AlertLabel.IsVisible = isWindSpeedHigh || isPrecipitationHigh;

            if (isWindSpeedHigh && isPrecipitationHigh)
            {
                AlertLabel.Text = "Warning: High wind speed and heavy precipitation!";
            }
            else if (isWindSpeedHigh)
            {
                AlertLabel.Text = "Warning: Wind speed is higher than 8 m/s!";
            }
            else if (isPrecipitationHigh)
            {
                AlertLabel.Text = "Warning: Precipitation is more than 10 mm/hr!";
            }
        }
    }
}
