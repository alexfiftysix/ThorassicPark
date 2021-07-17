using System;
using UnityEngine;

namespace World
{
    public class ParkArea : MonoBehaviour
    {
        [SerializeField] private GameObject northWall;
        [SerializeField] private GameObject eastWall;
        [SerializeField] private GameObject southWall;
        [SerializeField] private GameObject westWall;

        public void ClearWall(CompassDirection direction)
        {
            switch (direction)
            {
                case CompassDirection.North:
                    northWall.SetActive(false);
                    break;
                case CompassDirection.East:
                    eastWall.SetActive(false);
                    break;
                case CompassDirection.South:
                    southWall.SetActive(false);
                    break;
                case CompassDirection.West:
                    westWall.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}
