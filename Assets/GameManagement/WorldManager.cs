using System;
using UnityEngine;
using Utilities;
using Utilities.Extensions;
using World;

namespace GameManagement
{
    /// <summary>
    /// Manages building of the world
    /// Handles the whole deal with opening new park areas
    /// </summary>
    public class WorldManager : MonoBehaviour
    {
        private readonly ParkArea[,] _parkAreas = new ParkArea[3,3];

        public ParkArea parkAreaPrefab;
        public ParkArea firstArea;

        private Timer _parkExpansionTimer;
        private const int ParkExpansionTimeInSeconds = 10;

        // Start is called before the first frame update
        void Start()
        {
            _parkExpansionTimer = gameObject.AddTimer(ParkExpansionTimeInSeconds, () => ExpandPark(firstArea, CompassDirection.North));
            _parkAreas[1, 1] = firstArea;
        }

        private ParkArea BuildAreaAtCoordinates(int x, int y)
        {
            if (x > _parkAreas.GetLength(0) || y > _parkAreas.GetLength(1))
            {
                throw new IndexOutOfRangeException();
            }
            if (_parkAreas[x, y] is { }) return _parkAreas[x, y];

            // TODO: This is some fragile maths.
            //      Would be great if we could go negative and start at 0,0
            var position = new Vector3((x - 1) * 20, (y - 1) * 20, 0);
            var newArea = Instantiate(parkAreaPrefab, position, Quaternion.identity);
            newArea.xPosition = x;
            newArea.yPosition = y;
            _parkAreas[x, y] = newArea;
            return newArea;
        }

        private void ExpandPark(ParkArea source, CompassDirection direction)
        {
            _parkExpansionTimer.DeActivate();
            
            var directionVector2 = direction.ToVector2();
            var x = Convert.ToInt32(source.xPosition + directionVector2.X);
            var y = Convert.ToInt32(source.yPosition + directionVector2.Y);

            var newArea = BuildAreaAtCoordinates(x, y);
            source.Join(direction, newArea);
        }
    }
}
