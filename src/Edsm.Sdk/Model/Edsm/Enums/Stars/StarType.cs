using Newtonsoft.Json;

namespace Edsm.Sdk.Model.Edsm.Enums.Stars
{
    public class StarTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //var indicesOfCapitalLetters = value.ToString().IndexOf((c => char.IsUpper(c))

            writer.WriteValue(value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader?.Value?.ToString()
                ?.Replace(" Star", "")
                ?.Replace("(", "")
                ?.Replace(")", "")
                ?.Replace("-", "")
                ?.Replace("super giant", "supergiant")
                ?.Split(' ');

            if (value == null || value.Length == 0)
            {
                throw new Exception();
            }

            SpectralClass spectralClass;
            var colorClass = ColorClass.Unspecified;
            var massClass = MassClass.Unspecified;
            var specialClass = SpecialClass.None;

            if (value[0].Length == 1) // If the star is main sequence we can automate it
            {
                var spectralClassParseSuccess = Enum.TryParse(value[0], true, out spectralClass);

                if (!spectralClassParseSuccess)
                {
                    throw new Exception();
                }

                foreach (var component in value.Skip(1))
                {
                    var colorClassParseSuccess = Enum.TryParse<ColorClass>(component, true, out var colorClassTemp);

                    if (colorClassParseSuccess)
                    {
                        colorClass = colorClassTemp;
                        continue;
                    }

                    var massClassParseSuccess = Enum.TryParse<MassClass>(component, true, out var massClassTemp);

                    if (massClassParseSuccess)
                    {
                        massClass = massClassTemp;
                        continue;
                    }

                    var specialClassParseSuccess = Enum.TryParse<SpecialClass>(component, true, out var specialClassTemp);

                    if (specialClassParseSuccess)
                    {
                        specialClass = specialClassTemp;
                        continue;
                    }

                    throw new Exception();
                }
            }
            else // The star is something weird we need to process separately
            {
                switch (value[0])
                {
                    case "WolfRayet":
                        spectralClass = SpectralClass.W;
                        specialClass = SpecialClass.WolfRayet;
                        break;
                    case "White":
                        spectralClass = SpectralClass.D;
                        colorClass = ColorClass.White;
                        massClass = MassClass.Dwarf;

                        var whiteDwarfSubclass = new string(value[2].Skip(1).ToArray());
                        if (!string.IsNullOrWhiteSpace(whiteDwarfSubclass))
                        {
                            specialClass = Enum.Parse<SpecialClass>(whiteDwarfSubclass, true);
                        }
                        break;
                    case "Neutron":
                        spectralClass = SpectralClass.D;
                        specialClass = SpecialClass.Neutron;
                        break;
                    case "BlackHole":
                        spectralClass = SpectralClass.D;
                        specialClass = SpecialClass.BlackHole;
                        break;
                    default:
                        throw new Exception();
                }
            }

            return new StarType(spectralClass, massClass, colorClass, specialClass);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(StarType) || objectType == typeof(string);
        }
    }

    [JsonConverter(typeof(StarTypeConverter))]
    public class StarType
    {
        public readonly SpectralClass spectralClass;
        public readonly MassClass massClass;
        public readonly ColorClass colorClass;
        public readonly SpecialClass specialClass;

        public StarType(SpectralClass spectralClass, MassClass massClass, ColorClass colorClass, SpecialClass specialClass)
        {
            this.spectralClass = spectralClass;
            this.massClass = massClass;
            this.colorClass = colorClass;
            this.specialClass = specialClass;
        }
    }
}
