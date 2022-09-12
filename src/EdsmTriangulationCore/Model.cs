using Edsm.Sdk;
using Edsm.Sdk.Model.Edsm.Systems.System;

namespace EDSMTriangulationCore.Services
{
    public class Model : IModel
    {
        private readonly IEdsmClient _client;

        private readonly Dictionary<string, SphereSystemsResponse> _sphereSystemsCache = new();

        private readonly Dictionary<string, BodiesResponse> _targetDetailsCache = new();

        public HashSet<SphereSystemsRequest> Sources { get; private set; } = new();

        public HashSet<string> Targets { get; private set; } = new();

        public Model(IEdsmClient client)
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

        public async Task RemoveSource(string systemName, int min, int max)
        {
            var currentRequest = new SphereSystemsRequest(systemName, minRadius: min, radius: max);

            Sources.RemoveWhere(x => currentRequest.systemName == x.systemName && currentRequest.minRadius == x.minRadius && currentRequest.radius == x.radius);

            await UpdateTriangulation();
        }

        public async Task RemoveLastSource()
        {
            if (!Sources.Any())
            {
                return;
            }

            Sources.Remove(Sources.Last());

            await UpdateTriangulation();
        }

        public async Task<BodiesResponse> GetTargetDetails(string name)
        {
            if (_targetDetailsCache.ContainsKey(name))
            {
                return _targetDetailsCache[name];
            }
            else
            {
                
                var response = await _client.SendRequest<BodiesResponse>(new BodiesRequest(name));
                _targetDetailsCache.Add(name, response);
                return response;
            }
        }

        private async Task UpdateTriangulation()
        {
            if (!Sources.Any())
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

            if (_sphereSystemsCache.ContainsKey(requestKey))
            {
                return _sphereSystemsCache[requestKey].Select(x => x.name).ToHashSet();
            }
            else
            {
                var response = await _client.SendRequest<SphereSystemsResponse>(request);
                if (response == null)
                {
                    throw new Exception("Well, that didn't work");
                }
                _sphereSystemsCache.Add(requestKey, response);
                return response.Select(x => x.name).ToHashSet();
            }
        }
    }
}
