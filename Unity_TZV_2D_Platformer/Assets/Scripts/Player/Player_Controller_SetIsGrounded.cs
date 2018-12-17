using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_SetIsGrounded : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private FloatVariable groundedRadius;
    [SerializeField] private BoolVariable isPlayerGrounded;

    //****************************************************************************************************
    void Update()
    {
        // Assume we are falling
        isPlayerGrounded.value = false;

        // Create a list of colliders with the ground layer mask
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckTransform.position, groundedRadius.value, groundLayerMask);

        // If there is a collider that isn't ourselves then we are grounded
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isPlayerGrounded.value = true;
            }
        }
    }
}
