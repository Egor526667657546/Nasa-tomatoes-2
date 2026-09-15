using UnityEngine;
using System;
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
        targets = Physics.OverlapSphere(
            transform.position,
            agringArea,
            targetLayer
        );

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
        if (agent != null && Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
        {
            if (canAttack)
            {
                StartCoroutine(AnimationTimer());
            }
        }
        else
        {
            animator.SetBool("isWalking", true);

            agent.SetDestination(
                target.transform.position
            );
        }
    }

    public override void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            health = 0;

            Die();
        }
    }

    protected override void Die()
    {
        if (AnimManager.ActiveManager != null)
        {
            AnimManager.ActiveManager.EnemyDied();
        }

        Destroy(gameObject);
    }

    protected override void Attack(float dmg)
    {
        Debug.Log("alo");
        if (agent != null && Vector3.Distance(transform.position, target.transform.position) <= attackDistance * 2)
        {
            Debug.Log("alo11");
            target.GetComponent<PlayerHealthSystem>().TakeDamage(dmg);
        }
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