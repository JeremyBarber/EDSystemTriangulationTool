using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edsm.Sdk.Model.Edsm.Enums.Planets
{
    [JsonConverter(typeof(StandardEnumConverter<AtmosphereConstituent>))]
    public enum AtmosphereConstituent
    {
        Helium,
        Hydrogen,
        Methane,
        Neon
    }
}
