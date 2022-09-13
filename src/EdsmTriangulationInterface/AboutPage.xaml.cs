using CommunityToolkit.Maui.Views;

namespace EdsmTriangulationInterface;

public partial class AboutPage : Popup
{
	public AboutPage()
	{
		InitializeComponent();
	}

    public async void OnBugReportButtonClicked(object sender, EventArgs e)
    {
        var uri = new Uri("https://github.com/JeremyBarber/EDSystemTriangulationTool/issues");
        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }
}