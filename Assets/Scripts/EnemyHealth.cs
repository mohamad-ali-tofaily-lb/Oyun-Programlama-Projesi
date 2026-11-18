using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // This is the function the player will call
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("<color=red>ENEMY HIT! Health: " + currentHealth + "</color>");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died!");
        // We will add death animations later. For now, just make him disappear.
        gameObject.SetActive(false); 
    }
}