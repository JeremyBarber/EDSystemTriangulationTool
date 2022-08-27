using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Edsm.Sdk.Model.Edsm.Enums.Stars
{
    [JsonConverter(typeof(StandardEnumConverter<SpectralClass>))]
    public enum SpectralClass
    {
        O,
        B,
        A,
        F,
        G,
        K,
        M,
        L,
        T,
        Y,
        W,
        C,
        D
    }
}
