using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : EnemyBasic
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float projSpeed;

    private bool canAttack = true;
    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        LookForTarget();
        TurnTower();
    }
    private void LookForTarget()
    {
        targets = Physics.OverlapSphere(transform.position, agringArea, targetLayer);

        if (targets.Length > 0)
        {
            target = targets[0].gameObject;
            if (canAttack)
            {
                StartCoroutine(Timer());
            }
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
        Vector3 toEnemy = targets[0].gameObject.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, toEnemy);
        if (angle < Mathf.Abs(60))
        {
            GameObject bulletGO = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletGO.GetComponent<Bullet>().Init(targets[0].transform.position, dmg, projSpeed);
        }
    }
    private void TurnTower()
    {
        if (targets.Length <= 0 || targets[0] == null)
        {
            return;
        }
        GameObject selectedEnemy = targets[0].gameObject;
        var direction = selectedEnemy.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);

    }
    private IEnumerator Timer()
    {
        canAttack = false;
        Attack(damage);
        yield return new WaitForSeconds(cd);
        canAttack = true;

    }
}