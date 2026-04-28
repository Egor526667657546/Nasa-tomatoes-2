using UnityEngine;

public class HealthSystemBad : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    private void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        else
        {
            Effects(0);
        }
    }
    private void Die()
    {
        Effects(1);
        Destroy(gameObject);
    }
    private void Effects(int effectNum)
    {
        switch (effectNum)
        {
            case 0:
                //TakeDamageEffect
                break;
            case 1:
                //DieEffect
                break;
            default:
                break;
        }
    }
}
