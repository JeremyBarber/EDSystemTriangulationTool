using Edsm.Sdk;
using EDSMTriangulationTool;

try
{
    var client = new EdsmClient(new HttpClient());
    var model = new Model(client);
    var interactions = new UserInteractionHandler(model);

    interactions.EntryPoint();
}
catch (Exception ex)
{
    Console.WriteLine($"Something went wrong: {ex.Message}");
}