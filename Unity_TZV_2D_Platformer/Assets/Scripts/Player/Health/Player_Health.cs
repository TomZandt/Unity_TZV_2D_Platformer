using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameEvent playerDiedEvent;
    public GameEvent playerHealthChangedEvent;

    //****************************************************************************************************
    private void Start()
    {
        playerStats.playerHealth = playerStats.playerMaxHealth;

        playerHealthChangedEvent.Raise();
    }

    //****************************************************************************************************
    public void TakeDamage(int _damage)
    {
        playerStats.playerHealth -= _damage;

        playerHealthChangedEvent.Raise();

        if (playerStats.playerHealth < 0)
        {
            playerDiedEvent.Raise();

            Die();
        }
    }

    //****************************************************************************************************
    public void Die()
    {
        Destroy(gameObject);
    }
}
