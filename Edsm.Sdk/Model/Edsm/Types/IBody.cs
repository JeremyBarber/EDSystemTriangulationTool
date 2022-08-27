using Edsm.Sdk.Model.Edsm.Enums;
using Newtonsoft.Json;

namespace Edsm.Sdk.Model.Edsm.Types
{
    [JsonConverter(typeof(BodyConverter))]
    public abstract class IBody
    {
        public string name { get; set; }

        public BodyType type { get; set; }

        public int bodyId { get; set; }
    }
}