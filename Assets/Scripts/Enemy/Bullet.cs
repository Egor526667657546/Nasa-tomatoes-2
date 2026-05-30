using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage;

    private Rigidbody rb;
    private GameObject target;

    private bool isGoing = false;

    public GameObject Target { get => target; set => target = value; }


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
        Vector3 direction = (target.transform.position - transform.position).normalized;

        transform.forward = direction;
        rb.linearVelocity = transform.forward * speed;
        isGoing = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("damagable"))
        {
            collision.gameObject.GetComponent<PlayerHealthSystem>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
