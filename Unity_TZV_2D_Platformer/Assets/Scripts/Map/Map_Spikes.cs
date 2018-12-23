using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Spikes : MonoBehaviour
{
    private bool canHurt = true;
    private float attackRate = 1f;
    private int damage = 100;

    //****************************************************************************************************
    private void Start()
    {
        canHurt = true;
    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (canHurt)
        {
            if (_col.CompareTag("Enemy"))
            {
                if (_col.GetComponent<Enemy_Health>())
                {
                    _col.GetComponent<Enemy_Health>().TakeDamage(damage);
                }
            }
            else if (_col.CompareTag("Player"))
            {
                if (_col.GetComponent<Player_Health>())
                {
                    _col.GetComponent<Player_Health>().TakeDamage(damage);
                }
            }
        }
    }
}
