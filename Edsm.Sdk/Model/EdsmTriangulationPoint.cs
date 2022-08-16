namespace Edsm.Sdk.Model
{
    internal struct EdsmTriangulationPoint
    {
        public readonly string Name;
        public readonly int InnerRadius;
        public readonly int OuterRadius;

        public EdsmTriangulationPoint(string name, int innerRadius, int outerRadius)
        {
            Name = name;
            InnerRadius = innerRadius;
            OuterRadius = outerRadius;
        }

        public Dictionary<string, string> ToEdsmRequest()
        {
            return new Dictionary<string, string>
            {
                { "systemName", Name },
                { "radius", OuterRadius.ToString() },
                { "minRadius", InnerRadius.ToString() },
            };
        }
    }
}
