using CheckWeather.Services;
using Microsoft.Maui.Controls;
using System;

namespace CheckWeather
{
    public partial class WeatherAlert : ContentPage
    {
        private double latitude;
        private double longitude;

        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public WeatherAlert()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {


            base.OnAppearing();
            await GetYourLocation();
            await GetWeatherDataByLocation(latitude, longitude);
        }


        private async void OnFetchDataClicked(object sender, EventArgs e)
        {
            if (EntryLatitude.Text == null || EntryLongitude.Text == null)
            {
                await DisplayAlert("Error", "Latitude and Longitude cannot be null.", "OK");
                return;
            }
            double latitude = Convert.ToDouble(EntryLatitude.Text);
            double longitude = Convert.ToDouble(EntryLongitude.Text);

           await  DisplayAlert("Checking", $"Latitude: {latitude}, Longitude: {longitude}","OK");

            await GetWeatherDataByLocation(latitude, longitude);
        }




        public async Task GetYourLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    latitude = location.Latitude;
                    longitude = location.Longitude;
                }
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }


        private async void Tap_Location_Tapped(object sender, EventArgs e)
        {
            await GetYourLocation();
            await GetWeatherDataByLocation(latitude, longitude);
            EntryLatitude.Text = "";
            EntryLongitude.Text = "";


        }

        public async Task GetWeatherDataByLocation(double latitude, double longitude)
        {
            var result = await ApiService.GetCurrentWeather(latitude, longitude);
            if (result == null)
            {
                await DisplayAlert("SomeThing Went Wrong","Connection or Input Error", "Ok");
                LblRain.Text = "-- mm";
                LblWind.Text = "-- m/s";
                LblLocation.Text = "Error!";

                return;
            }
            LblRain.Text = result.current.precip_mm.ToString() + " mm";

            double windSpeedMps = result.current.wind_mph * 0.44704;

            LblWind.Text = windSpeedMps.ToString("F2") + " m/s";

            LblLocation.Text = $"{result.location.name},{result.location.country}";
            var iconUrl = result.current.condition.icon;
            if (iconUrl.StartsWith("//"))
            {
                iconUrl = "https:" + iconUrl;
            }


            LblIcon.Source = ImageSource.FromUri(new Uri(iconUrl));
            bool isPrecipitationHigh = result.current.precip_mm > 10;
            bool isWindSpeedHigh = windSpeedMps > 8;
            AlertLabel.IsVisible = isWindSpeedHigh || isPrecipitationHigh;
        
            if (isWindSpeedHigh && isPrecipitationHigh)
            {
                AlertLabel.Text = "Alert: High wind speed and heavy precipitation!";
            }
            else if (isWindSpeedHigh)
            {
                AlertLabel.Text = "Alert: Wind speed is higher than 8 m/s!";
            }
            else if (isPrecipitationHigh)
            {
                AlertLabel.Text = "Alert: Precipitation is more than 10 mm!";
            }



            if (AlertLabel.IsVisible)
            {
                await BlinkLabel(AlertLabel, 20);
            }

            async Task BlinkLabel(Label label, int numberOfBlinks)
            {
                for (int i = 0; i < numberOfBlinks; i++)
                {
                    await label.FadeTo(0, 250); // Fade out
                    await label.FadeTo(5, 250); // Fade in
                }
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
