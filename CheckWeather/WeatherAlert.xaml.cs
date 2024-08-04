using CheckWeather.Services;
using Microsoft.Maui.Controls;
using System;

namespace CheckWeather
{
    public partial class WeatherAlert : ContentPage
    {
        public WeatherAlert()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {


            base.OnAppearing();
            var result = await ApiService.GetCurrentWeather(27.7899, 83.501);
            LblRain.Text = result.current.precip_mm.ToString() + "mm/hr";

            double windSpeedMps = result.current.wind_mph * 0.44704;

            LblWind.Text = windSpeedMps.ToString("F2")+" m/s";


            bool isPrecipitationHigh = result.current.precip_mm > 10;
            bool isWindSpeedHigh = windSpeedMps > 8;
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


        //    private Random _random;
        //    private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(5); // Update every 5 seconds

        //    public WeatherAlert()
        //    {
        //        InitializeComponent();
        //        _random = new Random();
        //        StartUpdatingWeatherData();
        //    }

        //    private async void StartUpdatingWeatherData()
        //    {
        //        while (true)
        //        {
        //            GenerateRandomWeatherData();
        //            await Task.Delay(_updateInterval);
        //        }
        //    }

        //    private void GenerateRandomWeatherData()
        //    {
        //        double windSpeed = _random.NextDouble() * 20; 
        //        double precipitation = _random.NextDouble() * 20; 

        //        LblWind.Text = $"{windSpeed:F1} m/s";
        //        LblRain.Text = $"{precipitation:F1} mm/hr";

        //        bool isWindSpeedHigh = windSpeed > 8;
        //        bool isPrecipitationHigh = precipitation > 10;

        //        AlertLabel.IsVisible = isWindSpeedHigh || isPrecipitationHigh;

        //        if (isWindSpeedHigh && isPrecipitationHigh)
        //        {
        //            AlertLabel.Text = "Warning: High wind speed and heavy precipitation!";
        //        }
        //        else if (isWindSpeedHigh)
        //        {
        //            AlertLabel.Text = "Warning: Wind speed is higher than 8 m/s!";
        //        }
        //        else if (isPrecipitationHigh)
        //        {
        //            AlertLabel.Text = "Warning: Precipitation is more than 10 mm/hr!";
        //        }
        //    }


        //    private void OnFetchDataClicked(object sender, EventArgs e)
        //    {
        //        // Retrieve latitude and longitude values from Entry fields
        //        string latitude = EntryLatitude.Text;
        //        string longitude = EntryLongitude.Text;

        //        // Perform your data fetching logic here
        //        // You can use latitude and longitude values to fetch weather data

        //        // For demonstration, showing an alert with the entered values
        //        DisplayAlert("Fetching Data", $"Latitude: {latitude}, Longitude: {longitude}", "OK");
        //    }
        //}
    }
