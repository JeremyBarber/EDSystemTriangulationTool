using Edsm.Sdk.Model.Edsm.Enums;
using Edsm.Sdk.Model.Edsm.Types;

namespace EdsmTriangulationInterface.ModelView
{
    public class TargetDetailsViewModel
    {
        public string Name { get; private set; }

        public BodyType Type { get; private set; }

        public TargetDetailsViewModel(IBody body)
        {
            this.Name = body.name;
            this.Type = body.type;
        }
    }
}
