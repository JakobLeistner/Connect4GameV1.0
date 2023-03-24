using MauiClientPOC.Services;
using RestAPIClient;

namespace MauiClientPOC;

public partial class MainPage : ContentPage
{
	private readonly SignalRClient signalRClient;
	private readonly AuthProvider authProvider;
	int count = 0;

	public MainPage(SignalRClient signalRClient, AuthProvider authProvider)
	{
		InitializeComponent();
		this.signalRClient = signalRClient;
		this.authProvider = authProvider;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

