using Edsm.Sdk.Model.Edsm.Systems.System;

namespace EDSMTriangulationCore.Services
{
    public interface IModel
    {
        HashSet<SphereSystemsRequest> Sources { get; }
        HashSet<string> Targets { get; }

        Task<bool> CheckSourceExists(string systemName);
        Task AddSource(string systemName, int min, int max);
        Task DeleteLastSource();
        Task<BodiesResponse> GetTargetDetails(string name);
    }
}