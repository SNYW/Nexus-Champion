using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    private Camera mainCamera;
    public bool invert;

    private void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Ensure the camera is not null
        if (mainCamera != null)
        {
            // Get the direction from the sprite to the camera
            Vector3 directionToCamera =  transform.position - mainCamera.transform.position;

            directionToCamera = invert ? -directionToCamera : directionToCamera;
            // Make the sprite face the camera while keeping its original up direction
            transform.LookAt(transform.position + directionToCamera, Vector3.up);
        }
    }
}
