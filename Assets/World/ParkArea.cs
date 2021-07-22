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
        [SerializeField] private SpriteRenderer floor;

        public int xPosition;
        public int yPosition;
        // private float widthAndHeight = 20;
        
        private readonly Color _highlightColour = new Color(1, 0.92f, 0.016f, 0.3f);
        private readonly Color _transparentColour = new Color(0,0,0,0);

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

        public void Highlight()
        {
            floor.color = _highlightColour;
        }

        public void UnHighlight()
        {
            floor.color = _transparentColour;
        }
    }
}