using Newtonsoft.Json;

namespace Edsm.Sdk.Model.Edsm.Enums.Stars
{
    [JsonConverter(typeof(StandardEnumConverter<LuminosityClass>))]
    public enum LuminosityClass
    {
        I,
        II,
        III,
        IV,
        V,
        Va
    }
}
