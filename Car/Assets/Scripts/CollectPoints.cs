using UnityEngine;


public class PointsOnHit : MonoBehaviour
{
    [SerializeField] private bool destroyObjectOnCollision;
    [SerializeField] private VoidEventChannelSO voidEventChannel;
    [SerializeField] private AudioSource quack;

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 7)
        {
            quack.Play();
            if (destroyObjectOnCollision) Destroy(other.gameObject);
            voidEventChannel.RaiseEvent();
        }
    }
}
