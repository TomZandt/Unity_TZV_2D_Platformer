using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateMachine_Action_Attack", menuName = "TZV/ScriptableObjects/StateMachine_Action_Attack", order = 1)]
public class StateMachine_Action_Attack : StateMachine_Action
{
    //****************************************************************************************************
    public override void Act(StateMachine_Controller _controller)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_controller.transform.position, _controller.enemyStats.enemyAttackRadius, LayerMask.GetMask("Player"));
    
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                if (Time.unscaledTime >= _controller.attackTime)
                {
                    colliders[i].GetComponent<Player_Health>().TakeDamage(_controller.enemyStats.enemyAttackDamage);
                    _controller.enemyAttackGameEvent.Raise();
                    _controller.attackTime = Time.unscaledTime + _controller.enemyStats.enemyAttackRate;
                }
            }
        }
    }

    //****************************************************************************************************
    public override void FixedAct(StateMachine_Controller _controller)
    {
        
    }
}
