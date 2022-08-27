using Edsm.Sdk.Model.Edsm.Systems.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdsmTriangulationInterface.ModelView
{
    public class SourceViewModel
    {
        public string systemName { get; private set; }
        public int minRadius { get; private set; }
        public int radius { get; private set; }
        public string url { get; private set; }

        public SourceViewModel(SphereSystemsRequest system)
        {
            systemName = system.systemName;
            minRadius = system.minRadius;
            radius = system.radius;
            url = "https://static.edsm.net/img/favicons/android-chrome-192x192.png";
        }
    }
}
