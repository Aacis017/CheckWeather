namespace CheckWeather;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}
	private void BtnGetStarted_Click(object sender, EventArgs e)
	{
		Navigation.PushModalAsync(new WeatherAlert());
	
	}
}