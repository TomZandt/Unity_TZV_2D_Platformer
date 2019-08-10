using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GameEventHandler : MonoBehaviour 
{
    public PlayerStats playerStats;

    //****************************************************************************************************
    public void Player_Attack_GameEventHandler(GameObject playerWeapon)
    {
        playerWeapon.SetActive(true);
        StartCoroutine(Wait(playerStats.playerAttackRate, playerWeapon));
    }

    //****************************************************************************************************
    IEnumerator Wait(float _time, GameObject _playerWeapon)
    {
        yield return new WaitForSeconds(_time);
        _playerWeapon.SetActive(false);
    }
}
