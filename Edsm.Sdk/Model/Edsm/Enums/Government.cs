using Newtonsoft.Json;

namespace Edsm.Sdk.Model.Edsm.Enums
{
    [JsonConverter(typeof(StandardEnumConverter<Government>))]
    public enum Government
    {
        None,
        Anarchy,
        Communism,
        Confederacy,
        Cooperative,
        Corporate,
        Democracy,
        Dictatorship,
        Engineer,
        Feudal,
        Patronage,
        Prison,
        PrisonColony,
        PrivateOwnership,
        Theocracy
    }
}
