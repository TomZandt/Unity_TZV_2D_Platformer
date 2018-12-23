using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour 
{
    [SerializeField] private PlayerStats playerStats;

    //****************************************************************************************************
    private void Start()
    {
        playerStats.playerHealth = playerStats.playerMaxHealth;
    }

    //****************************************************************************************************
    public void TakeDamage(int _damage)
    {
        playerStats.playerHealth -= _damage;

        if (playerStats.playerHealth < 0)
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
