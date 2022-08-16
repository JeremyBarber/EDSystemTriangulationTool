using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edsm.Sdk.Model.Edsm.Systems.System
{
    public class SystemResponse : IEdsmResponse
    {
        public readonly string name;
        public readonly int id;
        public readonly long id64;
        public readonly Coords coords;
        public readonly bool coordsLocked;
        public readonly List<int> duplicates;
        public readonly bool requirePermit;
        public readonly string permitName;
        public readonly Information information;
        public readonly PrimaryStar primaryStar;
        public readonly DateTime hiddenAt;
        public readonly int mergedTo;

        public SystemResponse(
            string name,
            int id,
            long id64,
            Coords coords,
            bool coordsLocked,
            List<int> duplicates,
            bool requirePermit,
            string permitName,
            Information information,
            PrimaryStar primaryStar,
            DateTime hiddenAt,
            int mergedTo)
        {
            this.name = name;
            this.id = id;
            this.id64 = id64;
            this.coords = coords;
            this.coordsLocked = coordsLocked;
            this.duplicates = duplicates;
            this.requirePermit = requirePermit;
            this.permitName = permitName;
            this.information = information;
            this.primaryStar = primaryStar;
            this.hiddenAt = hiddenAt;
            this.mergedTo = mergedTo;
        }
    }
}
