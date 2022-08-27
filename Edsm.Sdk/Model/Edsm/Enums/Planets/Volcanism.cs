using Newtonsoft.Json;

namespace Edsm.Sdk.Model.Edsm.Enums.Planets
{
    [JsonConverter(typeof(StandardEnumConverter<Volcanism>))]
    public enum Volcanism
    {
        NoVolcanism,
        WaterGeysers,
        MajorWaterGeysers,
        MinorCarbonDioxideGeysers,
        CarbonDioxideGeysers
    }
}
