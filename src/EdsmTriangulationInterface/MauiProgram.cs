using CommunityToolkit.Maui;

namespace EdsmTriangulationInterface
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("EUROCAPS.ttf", "EuroCaps");
                });

            return builder.Build();
        }
    }
}