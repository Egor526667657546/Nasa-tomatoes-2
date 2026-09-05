using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 target;

    private float speed;
    private float damage;
    private float maxDistance;
    private bool isGoing = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        if (isGoing)
        {
            return;
        }
        Go();
    }
    private void Go()
    {
        Vector3 direction = (target - transform.position).normalized;

        transform.forward = direction;
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, Vector3.Distance(transform.position, target) / rb.linearVelocity.magnitude);
        isGoing = true;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("damagable"))
        {
            collision.gameObject.GetComponent<PlayerHealthSystem>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
    public void Init(Vector3 target, float damage, float speed)
    {
        this.target = target;
        this.damage = damage;
        this.speed = speed;
    }
}
