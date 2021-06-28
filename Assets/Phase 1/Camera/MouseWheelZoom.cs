using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phase_1.Camera
{
    public class MouseWheelZoom : MonoBehaviour
    {
        public UnityEngine.Camera myCamera;
        [SerializeField] private float zoomSpeed = 10f;
        [SerializeField] private float minZoom = 4f;
        [SerializeField] private float maxZoom = 15f;
 
        public void OnZoom(InputValue inputValue)
        {
            var val = inputValue.Get<Vector2>().y / 60 * -1;
            var goalSize = Math.Max(Math.Min(myCamera.orthographicSize + val, maxZoom), minZoom);
            myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize, goalSize, Time.deltaTime * zoomSpeed);
        }
    }
}
