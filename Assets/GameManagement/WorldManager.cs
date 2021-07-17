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
        private ParkArea[,] _parkAreas = new ParkArea[3,3];

        public ParkArea firstArea;
        public ParkArea northArea;
        
        private Timer _parkExpansionTimer;
        private const int ParkExpansionTimeInSeconds = 10;

        // Start is called before the first frame update
        void Start()
        {
            _parkExpansionTimer = gameObject.AddTimer(ParkExpansionTimeInSeconds, ExpandPark);
        }

        private void ExpandPark()
        {
            _parkExpansionTimer.DeActivate();
            firstArea.Join(CompassDirection.North, northArea);
        }
    }
}
