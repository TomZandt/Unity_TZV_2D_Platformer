using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Basic : MonoBehaviour
{
    public FloatVariable playerAttackRate;
    public FloatVariable attackDamage;

    private bool canFire = true;

    //****************************************************************************************************
    private void OnTriggerStay2D(Collider2D _col)
    {
        if (canFire && Input.GetButtonDown("Attack"))
        {
            if (_col.CompareTag("Enemy"))
            {
                if (_col.GetComponent<Enemy_Health>())
                {
                    canFire = false;
                    _col.GetComponent<Enemy_Health>().TakeDamage(attackDamage.value);
                    StartCoroutine(Wait(playerAttackRate.value));
                }
            }
        }
    }

    IEnumerator Wait(float _time)
    {
        yield return new WaitForSeconds(_time);
        canFire = true;
    }
}
