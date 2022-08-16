using Newtonsoft.Json;

namespace Edsm.Sdk.Model.Edsm.Enums
{
    [JsonConverter(typeof(StandardEnumConverter<Security>))]
    public enum Security
    {
        Anarchy,
        Low,
        Medium,
        High
    }
}
