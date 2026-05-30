using System.Collections;
using UnityEngine;

public class EnemyTurret : EnemyBasic
{
    [SerializeField] private GameObject bullet;
    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        LookForTarget();
    }
    private void LookForTarget()
    {
        targets = Physics.OverlapSphere(transform.position, agringArea, targetLayer);

        if (targets.Length > 0)
        {
            target = targets[0].gameObject;
            StartCoroutine(Timer());
        }
    }
    protected override void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
            health = 0;
        }
    }
    protected override void Die()
    {
        Destroy(gameObject);
    }
    protected override void Attack(float dmg)
    {
        Instantiate(bullet);
        bullet.GetComponent<Bullet>().Target = target;
        //target.TakeDamage(dmg);
    }
    private IEnumerator Timer()
    {
        //animationSignal
        yield return new WaitForSeconds(cd);
        Attack(damage);
    }
}