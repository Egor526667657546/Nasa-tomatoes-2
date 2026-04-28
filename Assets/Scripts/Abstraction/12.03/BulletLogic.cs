using System.Collections;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    private void Start()
    {
        StartCoroutine(BulletLife());
    }
    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
