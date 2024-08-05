using CheckWeather.Services;
using Microsoft.Maui.Controls;
using System;

namespace CheckWeather
{
    public partial class WeatherAlert : ContentPage
    {

        private double latitude;
        private double longitude;
        public WeatherAlert()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {


            base.OnAppearing();
           await GetYourLocation();
            var result = await ApiService.GetCurrentWeather(latitude, longitude);
            LblRain.Text = result.current.precip_mm.ToString() + "mm/hr";

            double windSpeedMps = result.current.wind_mph * 0.44704;

            LblWind.Text = windSpeedMps.ToString("F2")+" m/s";

            LblCity.Text = result.location.name;

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


        private async void OnFetchDataClicked(object sender, EventArgs e)
        {
            // Retrieve latitude and longitude values from Entry fields
            double latitude =  Convert.ToDouble( EntryLatitude.Text);
            double longitude = Convert.ToDouble(EntryLongitude.Text);

            var result = await ApiService.GetCurrentWeather(latitude, longitude);
            if (result != null)
            {
                LblRain.Text = result.current.precip_mm.ToString() + "mm/hr";

                double windSpeedMps = result.current.wind_mph * 0.44704;

                LblWind.Text = windSpeedMps.ToString("F2") + " m/s";

                LblCity.Text = result.location.name;

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


            
            DisplayAlert("D", $"Latitude: {latitude}, Longitude: {longitude}", "OK");
        }



        public async Task GetYourLocation()
        {
            var location = await Geolocation.GetLocationAsync();
           latitude= location.Latitude;
           longitude= location.Longitude;


        }

        private async void Tap_Location_Tapped(object sender, EventArgs e)
        {
            await GetYourLocation();
            await GetWeatherDataByLocation(latitude,longitude);
            

        }

        public async Task GetWeatherDataByLocation(double latitude,double longitude)
        {
            var result = await ApiService.GetCurrentWeather(latitude, longitude);
            LblRain.Text = result.current.precip_mm.ToString() + "mm/hr";

            double windSpeedMps = result.current.wind_mph * 0.44704;

            LblWind.Text = windSpeedMps.ToString("F2") + " m/s";

            LblCity.Text = result.location.name;

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
