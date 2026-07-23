using UnityEngine;

public abstract class EnemyBasic : MonoBehaviour
{
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float damage;
    [SerializeField] protected float agringArea;
    [SerializeField] protected float animationcd;
    [SerializeField] protected float cd;
    protected bool canThink = true;
    protected float health;

    protected Collider[] targets;
    protected GameObject target;

    protected abstract void TakeDamage(float dmg);
    protected abstract void Die();
    protected abstract void Attack(float dmg);

    public void StopOrResumeAI()
    {
        canThink = !canThink;
    }
}
