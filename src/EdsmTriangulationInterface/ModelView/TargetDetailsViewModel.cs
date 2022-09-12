using Edsm.Sdk.Model.Edsm.Enums;
using Edsm.Sdk.Model.Edsm.Types;
using Edsm.Sdk.Model.Types.Planets;

namespace EdsmTriangulationInterface.ModelView
{
    public class TargetDetailsViewModel
    {
        public string Name { get; private set; }

        public bool IsPlanet { get; private set; }

        public bool IsStar { get; private set; }

        public bool IsLandable { get; private set; }

        public TargetDetailsViewModel(IBody body)
        {
            this.Name = body.name;
            this.IsPlanet = body.type == BodyType.Planet;
            this.IsStar = !this.IsPlanet;
            this.IsLandable = this.IsPlanet && ((Planet)body).isLandable;
        }
    }
}
