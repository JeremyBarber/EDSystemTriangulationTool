using Edsm.Sdk.Model.Edsm.Enums;
using Edsm.Sdk.Model.Edsm.Enums.Stars;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Text.Json.Nodes;

namespace Edsm.Sdk.Model.Edsm.Types
{
    public class BodyConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //var indicesOfCapitalLetters = value.ToString().IndexOf((c => char.IsUpper(c))

            writer.WriteValue(value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //var baseType = serializer.Deserialize<Body>(reader);
            JObject obj = JObject.Load(reader);

            var bt = obj["type"].ToObject<BodyType>(serializer);
            //var bt = BodyType.Star;
            //var jsonObject = JObject.Load(reader);
            var output = default(IBody);
            switch (bt)
            {
                case BodyType.Star:

                    output = new Star();

                    break;
                case BodyType.Planet:
                    output = new Planet();
                    break;
                default:
                    throw new Exception($"Cannot figure out type {bt}");
            }
            serializer.Populate(obj.CreateReader(), output);
            return output;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(StarType) || objectType == typeof(string);
        }
    }

    //[JsonConverter(typeof(BodyConverter))]
    //public class Body
    //{
    //    public readonly string name;
    //    public readonly int? id;
    //    public readonly long id64;
    //    public readonly int bodyId;
    //    public readonly Discovery? discovery;
    //    public readonly BodyType type;
    //    public readonly Dictionary<string, int>?  parents;
    //    public readonly int? distanceToArrival;
    //    public readonly int? surfaceTemperature;
    //    public readonly double? orbitalPeriod;
    //    public readonly double? semiMajorAxis;
    //    public readonly double? orbitalEccentricity;
    //    public readonly double? orbitalInclination;
    //    public readonly double? argOfPeriapsis;
    //    public readonly double? rotationalPeriod;
    //    public readonly bool? rotationalPeriodTidallyLocked;
    //    public readonly float? axialTilt;
    //    public readonly DateTime? updateTime;
    //}
}
