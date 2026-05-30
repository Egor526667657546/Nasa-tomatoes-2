using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHp;

    private float health;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health < 0)
        {
            Die();
            health = 0;
        }
    }
    public void Heal(float heal)
    {
        health += heal;

        if (health > maxHp)
        {
            health = maxHp;
        }
    }
    private void Die()
    {

    }
}
