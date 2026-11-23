using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth { get; private set; }

    private EnemyAI ai;

    void Start()
    {
        currentHealth = maxHealth;
        ai = GetComponent<EnemyAI>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("<color=red>ENEMY HIT! Health: " + currentHealth + "</color>");
        
        if (ai != null) ai.ReceiveReward(-10f); // Negative reward for taking damage

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Debug.Log("Enemy Died!");
        gameObject.SetActive(false); 
    }
}