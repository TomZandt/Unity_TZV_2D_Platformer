using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Health : MonoBehaviour
{
    public ButtonStats buttonStats;
    private float currentHealth;

    public GameEvent buttonDiedEvent;

    //****************************************************************************************************
    private void Start()
    {
        currentHealth = buttonStats.buttonMaxHealth;
    }

    //****************************************************************************************************
    public void TakeDamage(float _damage)
    {
        currentHealth -= _damage;

        if (currentHealth < 0)
        {
            buttonDiedEvent.Raise();

            Die();
        }
    }

    //****************************************************************************************************
    public void Die()
    {
        Destroy(gameObject);
    }
}
