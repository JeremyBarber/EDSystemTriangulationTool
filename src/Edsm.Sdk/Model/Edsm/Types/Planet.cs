using Edsm.Sdk.Model.Edsm.Enums;
using Edsm.Sdk.Model.Edsm.Enums.Engineering;
using Edsm.Sdk.Model.Edsm.Enums.Planets;

namespace Edsm.Sdk.Model.Edsm.Types
{
    public sealed class Planet : IBody
    {
        public  string subType;
        public  bool isLandable;
        public  double gravity;
        public  double earthMasses;
        public  double radius;
        public  double? surfacePressure;
        public  Volcanism volcanismType;
        public  Atmosphere atmosphereType;
        public  Dictionary<AtmosphereConstituent, float> atmosphereComposition;
        public  Dictionary<SurfaceConstituent, float> solidComposition;
        public  Terraforming? terraformingState;
        public  Dictionary<Material, float> materials;
        public List<MineableRegion> rings;
        public Reserve? reserveLevel;

        //public  string name;
        public  int? id;
        public  long id64;
        //public  int bodyId;
        public  Discovery? discovery;
        //public  BodyType type;
        public List<KeyValuePair<string, int>>? parents;
        public  int? distanceToArrival;
        public  int? surfaceTemperature;
        public  double? orbitalPeriod;
        public  double? semiMajorAxis;
        public  double? orbitalEccentricity;
        public  double? orbitalInclination;
        public  double? argOfPeriapsis;
        public  double? rotationalPeriod;
        public  bool? rotationalPeriodTidallyLocked;
        public  float? axialTilt;
        public  DateTime? updateTime;

        //string IBody.name { get; set; }
        //BodyType IBody.type { get; set; }
        //int IBody.bodyId { get; set; }
    }
}
