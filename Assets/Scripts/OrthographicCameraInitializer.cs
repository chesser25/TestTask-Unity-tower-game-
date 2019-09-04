using UnityEngine;
using System;

namespace TowerGame
{
    public class OrthographicCameraInitializer : MonoBehaviour
    {
        [SerializeField] 
        private float coefficient;
        private void Update()
        {
            var mainCamera = gameObject.GetComponent<Camera>();
            if (!mainCamera)
                throw  new Exception("Add main camera");
            
            // Calculate correct orthographicSize for different device resolutions
            mainCamera.orthographicSize = coefficient * Screen.height / Screen.width * 0.5f;
        }
    }
}