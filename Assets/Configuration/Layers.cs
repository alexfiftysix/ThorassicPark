using System.Collections.Generic;

namespace Configuration
{
    public static class Layers
    {
        public static Dictionary<Layer, string> layers = new Dictionary<Layer, string>
        {
            {Layer.Default, "Default"},
            {Layer.Default, "TransparentFX"},
            {Layer.Default, "IgnoreRaycast"},
            {Layer.Default, "Player"},
            {Layer.Default, "Water"},
            {Layer.Default, "UI"},
            {Layer.Default, "Building"},
            {Layer.Default, "LineOfSight"},
            {Layer.Default, "Visitor"},
            {Layer.Default, "ParkWall"},
            {Layer.Default, "Camera"},
        };
    }

    public enum Layer
    {
        Default = 0,
        TransparentFX,
        IgnoreRaycast,
        Player,
        Water,
        UI,
        Building,
        LineOfSight,
        Visitor,
        ParkWall,
        Camera,
    }
}
