using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Basic : MonoBehaviour 
{
    public Transform attackPoint;
    public LayerMask layerMask;
    public FloatVariable attackRange;
    public FloatVariable attackDamage;

    private void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            Debug.Log("Attack");

            RaycastHit2D[] hit = Physics2D.RaycastAll(attackPoint.position, Vector2.right, attackRange.value, layerMask);

            Debug.DrawLine(attackPoint.position, attackPoint.position + new Vector3(attackRange.value, 0, 0), Color.red);

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.GetComponent<Enemy_Health>())
                {
                    hit[i].collider.GetComponent<Enemy_Health>().TakeDamage(attackDamage.value);
                }
            }
        }
    }
}
