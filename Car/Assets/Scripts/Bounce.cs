using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private int bouncePower;
    [SerializeField] private PrometeoCarController player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Rigidbody otherRigidbody = player.gameObject.GetComponent<Rigidbody>();

            // Preserve current horizontal velocities and add to the vertical component
            Vector3 currentVelocity = otherRigidbody.velocity;
            currentVelocity.y += bouncePower; // Add bouncePower only to the vertical component
            otherRigidbody.velocity = currentVelocity; // Assign the modified velocity back
        }
    }
}
