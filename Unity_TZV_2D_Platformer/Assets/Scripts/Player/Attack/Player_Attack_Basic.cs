using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Basic : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameEvent playerAttackGameEvent;

    //****************************************************************************************************
    private void Start()
    {
        playerStats.playerCanFire = true;
    }

    //****************************************************************************************************
    private void Update()
    {
        if (playerStats.playerCanFire && Input.GetButtonDown("Attack"))
        {
            playerStats.playerCanFire = false;

            playerAttackGameEvent.Raise();

            StartCoroutine(Wait(playerStats.playerAttackRate));
        }
    }

    //****************************************************************************************************
    private void OnTriggerStay2D(Collider2D _col)
    {
        if (playerStats.playerCanFire && Input.GetButtonDown("Attack"))
        {
            if (_col.CompareTag("Enemy"))
            {
                if (_col.GetComponent<Enemy_Health>())
                {
                    _col.GetComponent<Enemy_Health>().TakeDamage(playerStats.playerAttackDamage);
                }
            }
        }
    }

    //****************************************************************************************************
    IEnumerator Wait(float _time)
    {
        yield return new WaitForSeconds(_time);
        playerStats.playerCanFire = true;
    }
}