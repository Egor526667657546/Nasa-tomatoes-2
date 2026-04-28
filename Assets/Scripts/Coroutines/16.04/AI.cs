using System;
using System.Threading.Tasks;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int attacksNum;
    [SerializeField] private int cdBetweenAttackQueue;
    [SerializeField] private int generalCd;

    private Animator animator;
    private bool isAngry;
    private bool isAttacking;
    private void Start()
    {
        animator = enemy.GetComponent<Animator>();
        isAngry = false;
        isAttacking = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isAngry = !isAngry;
            Attack();
        }
    }
    private async void Attack()
    {
     

        while (isAngry)
        {
            int curAttack = 0;

            while (curAttack < attacksNum)
            {
                Debug.Log("1");
                animator.SetTrigger("attack");
                await Task.Delay(cdBetweenAttackQueue);
                curAttack++;
            }

            await Task.Delay(generalCd);
        }


    }
}
