using Edsm.Sdk.Model.Edsm.Enums.Stars;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edsm.Sdk.Model.Edsm.Enums.Engineering
{
    [JsonConverter(typeof(StandardEnumConverter<Material>))]
    public enum Material
    {
        Antimony,
        Arsenic,
        Cadmium,
        Carbon,
        Chromium,
        Iron,
        Germanium,
        Manganese,
        Mercury,
        Molybdenum,
        Nickel,
        Niobium,
        Phosphorus,
        Ruthenium,
        Selenium,
        Sulphur,
        Tellurium,
        Technetium,
        Tin,
        Tungsten,
        Vanadium,
        Yttrium,
        Zirconium,
        Zinc
    }
}
