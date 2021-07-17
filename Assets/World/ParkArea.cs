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

        public bool isOpen;
        // private float widthAndHeight = 20;

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

        /// <summary>
        /// Joins one area to another
        /// </summary>
        /// <param name="direction">Direction from 'this' area to the 'other' area</param>
        /// <param name="other">Park area to join 'this' to</param>
        public void Join(CompassDirection direction, ParkArea other)
        {
            ClearWall(direction);
            other.ClearWall(direction.Opposite());
        }
    }
}
