using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour 
{
    public FloatVariable health;
    private float currentHealth;

    //****************************************************************************************************
    private void Start()
    {
        currentHealth = health.value;
    }

    //****************************************************************************************************
    public void TakeDamage(float _damage)
    {
        currentHealth -= _damage;

        Debug.Log("Player health: " + currentHealth);

        if (currentHealth < 0)
        {
            Die();
        }
    }

    //****************************************************************************************************
    public void Die()
    {
        Destroy(gameObject);
    }
}
