using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameEvent playerDiedEvent;
    [SerializeField] private GameEvent playerHealthChangedEvent;

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
