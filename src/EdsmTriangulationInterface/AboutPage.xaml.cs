using CommunityToolkit.Maui.Views;

namespace EdsmTriangulationInterface;

public partial class AboutPage : Popup
{
    private readonly Uri issueUri = new Uri("https://github.com/JeremyBarber/EDSystemTriangulationTool/issues/new/choose");
    private readonly Uri eliteUri = new Uri("https://www.elitedangerous.com/");
    private readonly Uri edsmUri = new Uri("https://edsm.net/");
    private readonly Uri edassetsUri = new Uri("https://edassets.org/");

    public AboutPage()
	{
		InitializeComponent();
	}

    public async void OnBugReportButtonClicked(object sender, EventArgs e)
    {
        await OpenUrl(issueUri);
    }

    public async void OnEliteButtonClicked(object sender, EventArgs e)
    {
        await OpenUrl(eliteUri);
    }

    public async void OnEDSMButtonClicked(object sender, EventArgs e)
    {
        await OpenUrl(edsmUri);
    }

    public async void OnEDAssetsButtonClicked(object sender, EventArgs e)
    {
        await OpenUrl(edassetsUri);
    }

    private async Task OpenUrl(Uri uri)
    {
        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }
}