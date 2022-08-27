namespace EdsmTriangulationInterface
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("EUROCAPS.ttf", "EuroCaps");
                });

            return builder.Build();
        }
    }
}