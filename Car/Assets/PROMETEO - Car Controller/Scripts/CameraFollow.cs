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
    private float rotationY = 0f;
    private float rotationX = 0f;

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

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Always look at the car
        transform.LookAt(carTransform.position + Vector3.up * 1.5f); // Slightly above the car's center for a better view

        // Handle mouse input to adjust the rotation around the car
        HandleMouseRotation();
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
}
