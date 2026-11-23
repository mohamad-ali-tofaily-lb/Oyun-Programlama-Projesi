using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("<color=red>ENEMY HIT! Health: " + currentHealth + "</color>");

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Debug.Log("Enemy Died!");
        gameObject.SetActive(false); 
    }
}