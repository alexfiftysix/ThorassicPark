using System.Collections.Generic;

namespace Configuration
{
    public static class Configuration
    {
        public static readonly Dictionary<Layer, string> Layers = new Dictionary<Layer, string>
        {
            {Layer.Default, "Default"},
            {Layer.TransparentFX, "TransparentFX"},
            {Layer.IgnoreRaycast, "IgnoreRaycast"},
            {Layer.Player, "Player"},
            {Layer.Water, "Water"},
            {Layer.UI, "UI"},
            {Layer.Building, "Building"},
            {Layer.LineOfSight, "LineOfSight"},
            {Layer.Visitor, "Visitor"},
            {Layer.ParkWall, "ParkWall"},
            {Layer.Camera, "Camera"},
        };

        public static readonly Dictionary<Scene, int> Scenes = new Dictionary<Scene, int>
        {
            {Scene.MainMenu, 0},
            {Scene.DeckBuild, 1},
            {Scene.Game, 2},
            {Scene.PostGame, 3},
            {Scene.TestScene, 4},
        };
    }
}