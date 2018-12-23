using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GameEventHandler : MonoBehaviour 
{
    [SerializeField] private EnemyStats enemyStats;

    //****************************************************************************************************
    public void Enemy_Attack_GameEventHandler(GameObject enemyWeapon)
    {
        if (enemyWeapon.GetComponentInParent<Transform>().gameObject == gameObject)
        {
            enemyWeapon.SetActive(true);
            StartCoroutine(Wait(enemyStats.enemyAttackRate, enemyWeapon));
        }
    }

    //****************************************************************************************************
    IEnumerator Wait(float _time, GameObject _enemyWeapon)
    {
        yield return new WaitForSeconds(_time);
        _enemyWeapon.SetActive(false);
    }
}
