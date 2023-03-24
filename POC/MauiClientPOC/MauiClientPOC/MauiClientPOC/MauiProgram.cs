using MauiClientPOC.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MauiClientPOC;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//DependencyInjection
		
		builder.Services.AddTransient<MainPage>();
        RestAPIClient.RestApiClient restApiClient = new RestAPIClient.RestApiClient(() => "https://localhost:5515/");
        RestAPIClient.AuthProvider authProvider = new RestAPIClient.AuthProvider(restApiClient);
        builder.Services.AddSingleton(authProvider);
        builder.Services.AddSingleton(new SignalRClient());

		return builder.Build();
	}
}
