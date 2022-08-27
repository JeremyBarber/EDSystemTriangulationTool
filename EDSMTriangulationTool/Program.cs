using Edsm.Sdk;
using EDSMTriangulationCore.Services;
using EDSMTriangulationTool.Services;
using Microsoft.Extensions.DependencyInjection;

try
{
    var serviceCollection = new ServiceCollection();

    serviceCollection.AddHttpClient();
    serviceCollection.AddTransient<IEdsmClient, EdsmClient>();
    serviceCollection.AddTransient<IModel, Model>();
    serviceCollection.AddSingleton<IUserInteractionHandler, UserInteractionHandler>();

    var serviceProvider = serviceCollection.BuildServiceProvider();

    var userInteractionHandler = serviceProvider.GetService<IUserInteractionHandler>();

    userInteractionHandler?.EntryPoint();
}
catch (Exception ex)
{
    Console.WriteLine($"Apologies CMDR, an unrecoverable error has been detected and the program needs to exit: {ex.Message}");
    Console.ReadKey();
}