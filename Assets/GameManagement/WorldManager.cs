using System;
using System.Linq;
using Common.Utilities;
using Common.Utilities.Extensions;
using UnityEngine;
using World;

namespace GameManagement
{
    /// <summary>
    /// Manages building of the world
    /// Handles the whole deal with opening new park areas
    /// </summary>
    public class WorldManager : MonoBehaviour
    {
        private readonly ParkArea[,] _parkAreas = new ParkArea[3, 3];
        private ParkArea _selectedParkArea;

        public ParkArea parkAreaPrefab;
        public ParkArea firstArea;

        private Timer _parkExpansionTimer;
        private const int ParkExpansionTimeInSeconds = 60;

        public GameObject parkExpansionMenu;

        private UnityEngine.Camera _mainCamera;
        private float _previousOrthographicSize;

        private int _areasCount = 1;
        private int _maxAreas = 4;

        // Start is called before the first frame update
        void Start()
        {
            _parkExpansionTimer = gameObject.AddTimer(ParkExpansionTimeInSeconds, ShowParkExpansionMenu);
            _parkAreas[1, 1] = firstArea;
            parkExpansionMenu.SetActive(false);
            _mainCamera = FindObjectOfType<UnityEngine.Camera>();
            gameObject.GetComponent<GameManager>().OnParkBreaks += (sender, args) => OnParkBreaks();
        }

        // TODO: why can't you just use Expand(CompassDirection) in the editor?
        public void ExpandNorth()
        {
            Expand(CompassDirection.North);
        }

        public void ExpandEast()
        {
            Expand(CompassDirection.East);
        }

        public void ExpandSouth()
        {
            Expand(CompassDirection.South);
        }

        public void ExpandWest()
        {
            Expand(CompassDirection.West);
        }

        public void Expand(CompassDirection direction)
        {
            ExpandPark(_selectedParkArea, direction);
            HideParkExpansionMenu();
        }

        private void ExpandPark(ParkArea source, CompassDirection direction)
        {
            var directionVector2 = direction.ToVector2();
            var x = Convert.ToInt32(source.xPosition + directionVector2.X);
            var y = Convert.ToInt32(source.yPosition + directionVector2.Y);

            var newArea = BuildAreaAtCoordinates(x, y);
            source.Join(direction, newArea);
            _areasCount++;
            if (_areasCount >= _maxAreas)
            {
                _parkExpansionTimer.DeActivate();
            }
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

        private void ShowParkExpansionMenu()
        {
            _selectedParkArea = _parkAreas.Flatten().Where(p => !(p is null)).RandomChoice();
            _previousOrthographicSize = _mainCamera.orthographicSize;
            _mainCamera.orthographicSize = 50;
            TimeControl.Pause();
            parkExpansionMenu.SetActive(true);
            _selectedParkArea.Highlight();
        }

        private void HideParkExpansionMenu()
        {
            _mainCamera.orthographicSize = _previousOrthographicSize;
            TimeControl.UnPause();
            parkExpansionMenu.SetActive(false);
            _selectedParkArea.UnHighlight();
        }

        private void OnParkBreaks()
        {
            _parkExpansionTimer.DeActivate();
        }
    }
}