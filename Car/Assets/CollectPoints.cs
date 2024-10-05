using System.Collections;
using UnityEngine;


public class PointsOnHit : MonoBehaviour
{
    [SerializeField] private bool destroyObjectOnCollision;
    [SerializeField] private VoidEventChannelSO voidEventChannel;
    [SerializeField] private float bobbingHeight = 0.5f; // Maximum height for bobbing
    [SerializeField] private float bobbingSpeed = 2f; // Speed of bobbing
    [SerializeField] private float spinSpeed = 100f; // Speed of spinning

    private void Start()
    {
        // Start the bobbing coroutine when the object is created
        StartCoroutine(BobAndSpin());
    }

    private IEnumerator BobAndSpin()
    {
        // Initial position
        Vector3 startPos = transform.position;

        while (true) // Run indefinitely
        {
            // Calculate new bobbing position
            float newY = startPos.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;

            // Clamp the new Y position to not go below 0
            newY = Mathf.Max(newY, 0);

            // Apply the new position while keeping X and Z the same
            transform.position = new Vector3(startPos.x, newY, startPos.z);

            // Rotate the object
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime); // Spin around the Y axis

            yield return null; // Wait for the next frame
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 7)
        {
            if (destroyObjectOnCollision) Destroy(other.gameObject);
            voidEventChannel.RaiseEvent();
        }
    }
}
