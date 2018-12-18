using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StateMachine_Decision_Look : StateMachine_Decision
{
    //****************************************************************************************************
    public override bool Decide(StateMachine_Controller _controller)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_controller.transform.position, _controller.enemyStats.playerDetectRadius, LayerMask.GetMask("Player"));

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                _controller.chaseTarget = colliders[i].transform;
                return true;
            }
        }

        return false;
    }
}
