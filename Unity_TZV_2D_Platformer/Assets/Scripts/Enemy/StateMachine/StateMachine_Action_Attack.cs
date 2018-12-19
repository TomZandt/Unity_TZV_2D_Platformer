using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StateMachine_Action_Attack : StateMachine_Action
{
    private float attackTime = 0f;

    public override void Act(StateMachine_Controller _controller)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_controller.transform.position, _controller.enemyStats.enemyAttackRadius, LayerMask.GetMask("Player"));

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                if (Time.time >= attackTime)
                {
                    Debug.Log("I attacked you");

                    attackTime = Time.time + _controller.enemyStats.enemyAttackRate;
                }
            }
        }
    }
}
