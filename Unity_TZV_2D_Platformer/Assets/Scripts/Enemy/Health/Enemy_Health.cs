using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour 
{
    public EnemyStats enemyStats;
    private float currentHealth;

    //****************************************************************************************************
    private void Start()
    {
        currentHealth = enemyStats.enemyMaxHealth;
    }

    //****************************************************************************************************
    public void TakeDamage(float _damage)
    {
        currentHealth -= _damage;

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
