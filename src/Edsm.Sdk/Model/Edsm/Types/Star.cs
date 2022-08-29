using Edsm.Sdk.Model.Edsm.Enums;
using Edsm.Sdk.Model.Edsm.Enums.Stars;

namespace Edsm.Sdk.Model.Edsm.Types
{
    public class Star : IBody
    {
        public  StarType subType;
        public  bool isScoopable;
        public  bool? isMainStar;
        public  int? age;
        public  SpectralClassDetailed? spectralClass;
        public  LuminosityClass? luminosity;
        public  double? absoluteMagnitude;
        public  double? solarMasses;
        public  double? solarRadius;
        public  List<MineableRegion> belts;

        //public  string name;
        public  int? id;
        public  long id64;
        //public  int bodyId;
        public  Discovery? discovery;
        //public  BodyType type;
        public  List<KeyValuePair<string, int>>? parents;
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

        public class StarShort
        {
            public  string name;
            public  StarType type;
            public  bool isScoopable;

            public StarShort(string name, StarType type, bool isScoopable)
            {
                this.name = name;
                this.type = type;
                this.isScoopable = isScoopable;
            }
        }
    }
}
