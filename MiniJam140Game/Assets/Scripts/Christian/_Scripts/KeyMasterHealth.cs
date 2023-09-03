using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMasterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Implement logic for Keymaster's death, such as destroying the Keymaster or playing a death animation.
        Destroy(gameObject);
    }
}
