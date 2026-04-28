using UnityEngine;

public class PhysicsBody : MonoBehaviour, IPhysicsBody
{
    [SerializeField] private Rigidbody rb;
    public void Explode(Vector3 dir)
    {
        rb.AddForce(dir);
    }
}
