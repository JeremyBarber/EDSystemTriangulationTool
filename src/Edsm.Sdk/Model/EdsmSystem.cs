using System.Diagnostics.CodeAnalysis;

namespace Edsm.Sdk.Model
{
    internal struct EdsmSystem
    {
        public readonly string Name;
        public readonly float Distance;
        public readonly int BodyCount;

        public EdsmSystem(string name, float distance, int bodyCount)
        {
            Name = name;
            Distance = distance;
            BodyCount = bodyCount;
        }

        public override int GetHashCode() => Name.GetHashCode();

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is EdsmSystem other && Equals(other);

        public bool Equals(EdsmSystem other) => Name.Equals(other.Name);
    }
}
