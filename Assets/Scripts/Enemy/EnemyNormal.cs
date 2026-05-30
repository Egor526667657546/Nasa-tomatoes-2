using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;

public class EnemyNormal : EnemyBasic
{
    [SerializeField] private float attackDistance;

    private NavMeshAgent agent;


    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
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
            GoTowardsTarget();
        }
    }
    private void GoTowardsTarget()
    {
        if (agent != null && MathF.Abs(Vector3.Distance(gameObject.transform.position, target.transform.position)) <= attackDistance)
        {
            StartCoroutine(Timer());
        }
        else
        {
            agent.SetDestination(target.transform.position);
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
        //target.TakeDamage(dmg);
    }
    private IEnumerator Timer()
    {
        //animationSignal
        yield return new WaitForSeconds(cd);
        Attack(damage);
    }
}
