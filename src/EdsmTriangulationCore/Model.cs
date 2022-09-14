using Edsm.Sdk;
using Edsm.Sdk.Dto;
using Edsm.Sdk.Model.Edsm.Systems.System;
using EdsmTriangulationCore;

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

            var response = await ClientSendRequestSafe<SystemResponse>(request);

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
                
                var response = await ClientSendRequestSafe<BodiesResponse>(new BodiesRequest(name));
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
                var response = await ClientSendRequestSafe<SphereSystemsResponse>(request);
                _sphereSystemsCache.Add(requestKey, response);
                return response.Select(x => x.name).ToHashSet();
            }
        }

        private async Task<T?> ClientSendRequestSafe<T>(IEdsmRequest request, bool throwIfEmpty = false) where T : IEdsmResponse
        {
            try
            {
                var response = await _client.SendRequest<T>(request);

                if (throwIfEmpty && response == null)
                {
                    throw new EdsmNullResponseException(
                        "The response to an EDSM query was empty. " +
                        "This usually indicates that you are trying to reference a system or body that does not exist. " +
                        "If this problem persists, then please consider opening a bug report.");
                }

                return response;
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                throw new EdsmConnectionException(
                        "EDSM did not response in a timely manner to the request. " +
                        "Please check your device is able to reach the internet, as that is a requirement for the app to function. " +
                        "If you have checked your connection but are still seeing this, then please consider opening a bug report.");
            }
        }
    }
}
