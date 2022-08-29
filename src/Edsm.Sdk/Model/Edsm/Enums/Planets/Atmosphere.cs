using Newtonsoft.Json;

namespace Edsm.Sdk.Model.Edsm.Enums.Planets
{
    [JsonConverter(typeof(StandardEnumConverter<Atmosphere>))]
    public enum Atmosphere
    {
        NoAtmosphere,
        ThinMethane,
        Methane,
        ThickMethane,
        NeonRich
    }
}
