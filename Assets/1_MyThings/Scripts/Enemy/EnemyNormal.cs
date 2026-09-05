using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;

public class EnemyNormal : EnemyBasic
{
    [SerializeField] private Animator animator;
    [SerializeField] private float attackDistance;

    private NavMeshAgent agent;
    private bool canAttack = true;
   

    private void Awake()
    {
        if (gameObject.TryGetComponent<ObjectSlower>(out ObjectSlower os))
        {
            canThink = false;
        }
    }

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        health = maxHealth;
    }
    private void Update()
    {
        if (canThink)
        {
            LookForTarget();
        }
    }
    private void LookForTarget()
    {
        targets = Physics.OverlapSphere(transform.position, agringArea, targetLayer);

        if (targets.Length > 0)
        {
            target = targets[0].gameObject;
            GoTowardsTarget();
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    private void GoTowardsTarget()
    {
        if (agent != null && MathF.Abs(Vector3.Distance(gameObject.transform.position, target.transform.position)) <= attackDistance && canAttack)
        {
            StartCoroutine(AnimationTimer());
        }
        else
        {
            animator.SetBool("isWalking", true);
            agent.SetDestination(target.transform.position);
        }
    }
    public override void TakeDamage(float dmg)
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
        target.GetComponent<PlayerHealthSystem>().TakeDamage(dmg);
    }
    private IEnumerator AnimationTimer()
    {
        canAttack = false;
        animator.SetBool("isWalking", false);
        animator.SetTrigger("punch");
        yield return new WaitForSeconds(animationcd);
        Attack(damage);
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(cd);
        canAttack = true;
    }
}
