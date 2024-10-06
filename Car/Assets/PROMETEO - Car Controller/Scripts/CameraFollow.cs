using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform carTransform;

    [Range(1, 100)]
    public float followSpeed = 15;

    [Range(1, 100)]
    public float lookSpeed = 75;

    [Range(1, 100)]
    public float distanceBehindCar = 10;

    [Range(1, 100)]
    public float heightAboveCar = 5;

    public float mouseSensitivity = 3f;

    public float mouseScrollSpeed = 3f;

    private float rotationY = 0f;
    private float rotationX = 0f;

    public float minHeight = 1f; // Minimum height above the ground

    void Start()
    {
        // Set the initial rotation values
        Vector3 angles = transform.eulerAngles;
        rotationY = angles.y;
        rotationX = angles.x;
    }

    void LateUpdate()
    {
        // Calculate the offset relative to the car's position
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 offset = rotation * new Vector3(0, heightAboveCar, -distanceBehindCar);

        // Set the target position behind the car
        Vector3 targetPosition = carTransform.position + carTransform.TransformDirection(offset);

        // Perform a raycast to determine if the camera is going below the ground or into an obstacle
        RaycastHit hit;
        if (Physics.Raycast(carTransform.position, (targetPosition - carTransform.position).normalized, out hit, offset.magnitude))
        {
            // Adjust the target position to stay above the hit point
            targetPosition.y = Mathf.Max(hit.point.y + minHeight, targetPosition.y);
        }

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Always look at the car
        transform.LookAt(carTransform.position + carTransform.up * 1.5f); // Use car's up direction to account for car tilt

        // Handle mouse input to adjust the rotation around the car
        HandleMouseRotation();

        // Handle scroll input to adjust the camera distance
        HandleScroll();
    }

    void HandleMouseRotation()
    {
        // Get the mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Adjust the rotation angles based on mouse input
        rotationY += mouseX;
        rotationX -= mouseY;

        // Clamp the vertical rotation to avoid excessive camera movement
        rotationX = Mathf.Clamp(rotationX, -10f, 60f); // Adjust the min and max values for desired vertical angle limits
    }

    void HandleScroll()
    {
        distanceBehindCar -= Input.mouseScrollDelta.y * mouseScrollSpeed;
        distanceBehindCar = Mathf.Clamp(distanceBehindCar, 15, 50);
    }
}
