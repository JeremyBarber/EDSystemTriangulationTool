using Newtonsoft.Json;

namespace Edsm.Sdk.Model.Edsm.Enums
{
    [JsonConverter(typeof(StandardEnumConverter<FactionState>))]
    public enum FactionState
    {
        Blight,
        Boom,
        Bust,
        CivilLiberty,
        CivilUnrest,
        CivilWar,
        Drought,
        Election,
        Exiled,
        Expansion,
        Famine,
        InfrastructureFailure,
        Investment,
        Lockdown,
        NaturalDisaster,
        None,
        Outbreak,
        PirateAttack,
        PublicHoliday,
        Retreat,
        TerroristAttack,
        War
    }
}
