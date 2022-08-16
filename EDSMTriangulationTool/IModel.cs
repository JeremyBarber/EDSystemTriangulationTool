using Edsm.Sdk.Model.Edsm.Systems.System;

namespace EDSMTriangulationTool
{
    internal interface IModel
    {
        HashSet<SphereSystemsRequest> Sources { get; }
        HashSet<string> Targets { get; }

        Task AddSource(string systemName, int min, int max);
        Task<bool> CheckSourceExists(string systemName);
        Task DeleteLastSource();
    }
}