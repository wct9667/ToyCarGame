using UnityEngine;


public class PointsOnHit : MonoBehaviour
{
    [SerializeField] private bool destroyObjectOnCollision;
    [SerializeField] private VoidEventChannelSO voidEventChannel;

    private void Start()
    {
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
