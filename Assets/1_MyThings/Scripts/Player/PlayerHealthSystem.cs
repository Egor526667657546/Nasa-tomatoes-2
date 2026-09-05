using System;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHp;

    private float health;

    private void Awake()
    {
        health = maxHp;
        Time.timeScale = 1;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log($"-{dmg} health");
        Debug.Log($"Player health: {health}");
        if (health <= 0)
        {
            UIManager.OnPlayerDie.Invoke();
            health = 0;
            Die();
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
        Cursor.lockState = CursorLockMode.Confined;
        CameraMove.OnPause.Invoke();
        Time.timeScale = 0;
        //Destroy(gameObject);
    }
}
