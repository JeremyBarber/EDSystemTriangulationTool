using Edsm.Sdk;
using Edsm.Sdk.Model.Edsm.Systems.System;

namespace EDSMTriangulationTool
{
    internal class Model
    {
        private readonly EdsmClient _client = new EdsmClient(new HttpClient());

        private readonly Dictionary<string, SphereSystemsResponse> Cache = new();

        public HashSet<SphereSystemsRequest> Sources { get; private set; } = new();

        public HashSet<string> Targets { get; private set; } = new();

        public Model(EdsmClient client)
        {
            _client = client;
        }

        public async Task<bool> CheckSourceExists(string systemName)
        {
            var request = new SystemRequest(systemName);

            var response = await _client.SendRequest<SystemResponse>(request);

            return response != null;
        }

        public async Task AddSource(string systemName, int min, int max)
        {
            var newRequest = new SphereSystemsRequest(systemName, minRadius: min, radius: max);

            Sources.Add(newRequest);

            await UpdateTriangulation();
        }

        public async Task DeleteLastSource()
        {
            if (Sources.Count == 0)
            {
                return;
            }

            Sources.Remove(Sources.Last());

            await UpdateTriangulation();
        }

        private async Task UpdateTriangulation()
        {
            if (Sources.Count == 0)
            {
                Targets = new HashSet<string>();
                return;
            }
            
            var targetList = await QuerySystemsResponseCache(Sources.First());

            foreach (var source in Sources.Skip(1))
            {
                targetList.IntersectWith(await QuerySystemsResponseCache(source));
            }

            Targets = targetList;
        }

        private async Task<HashSet<string>> QuerySystemsResponseCache(SphereSystemsRequest request)
        {
            var requestKey = $"{request.systemName}-{request.minRadius}-{request.radius}";

            if (Cache.ContainsKey(requestKey))
            {
                return Cache[requestKey].Select(x => x.name).ToHashSet();
            }
            else
            {
                var response = await _client.SendRequest<SphereSystemsResponse>(request);
                Cache.Add(requestKey, response);
                return response.Select(x => x.name).ToHashSet();
            }
        }
    }
}
