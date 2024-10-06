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

    public float minHeight = 1f; // Minimum height above the ground

    private List<Renderer> obstructedObjects = new List<Renderer>(); // List to keep track of objects that have been made transparent

    void Start()
    {
        // Set the initial rotation values based on the current camera orientation
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
        transform.LookAt(carTransform.position + carTransform.up * 1.5f); // Use car's up direction to account for car tilt

        // Handle mouse input to adjust the rotation around the car
        HandleMouseRotation();

        // Handle occlusion - make obstructing objects transparent
        HandleOcclusion();
    }

    void HandleMouseRotation()
    {
        // Get the mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Adjust the rotation angles based on mouse input
        rotationY += mouseX;
        rotationX -= mouseY;

        // Clamp the vertical rotation to avoid excessive camera movement (limits looking too far up or down)
        rotationX = Mathf.Clamp(rotationX, -10f, 60f);
    }

    // Function to handle making obstructing objects transparent
    void HandleOcclusion()
    {
        // Restore the transparency of objects that were previously made transparent
        foreach (Renderer renderer in obstructedObjects)
        {
            SetObjectTransparency(renderer, 1.0f); // Set fully opaque
        }
        obstructedObjects.Clear(); // Clear the list of previously obstructed objects

        // Perform a raycast to detect objects between the camera and the car
        Vector3 directionToCar = carTransform.position - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToCar.normalized, directionToCar.magnitude);

        // Iterate through all objects hit by the raycast
        foreach (RaycastHit hit in hits)
        {
            // Skip the car itself to prevent making the car transparent
            if (hit.transform == carTransform)
                continue;

            // Get the renderer of the object hit
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Set the object to be partially transparent
                SetObjectTransparency(renderer, 0.3f);
                // Add the renderer to the list so its transparency can be restored later
                obstructedObjects.Add(renderer);
            }
        }
    }

    // Function to set the transparency of an object
    void SetObjectTransparency(Renderer renderer, float alpha)
    {
        // Get the material of the renderer to modify its properties
        Material material = renderer.material;

        // Change the rendering mode to transparent if needed
        if (alpha < 1.0f)
        {
            // Set the material to use transparency settings
            material.SetFloat("_Mode", 3); // Set to transparent mode
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000; // Render after opaque geometry
        }
        else
        {
            // Reset to opaque mode if the alpha is 1 (fully opaque)
            material.SetFloat("_Mode", 0);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 2000; // Render with opaque geometry
        }

        // Set the alpha value of the material color
        Color color = material.color;
        color.a = alpha;
        material.color = color;
    }
}

