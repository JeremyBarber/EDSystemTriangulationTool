using Edsm.Sdk.Model.Edsm.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Edsm.Sdk.Model.Edsm.Systems
{
    public struct PrimaryStar
    {
        public readonly string name;
        public readonly StarType type;
        public readonly bool isScoopable;

        public PrimaryStar(StarType type, string name, bool isScoopable)
        {
            this.type = type;
            this.name = name;
            this.isScoopable = isScoopable;
        }
    }
}
