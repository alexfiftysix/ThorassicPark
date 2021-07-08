using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class Directions
    {
        public static List<Vector2> directions = new List<Vector2>()
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            new Vector2(1, 1),
            new Vector2(1, -1),
            new Vector2(-1, 1),
            new Vector2(-1, -1),
            Vector2.zero,
        };
    }
}