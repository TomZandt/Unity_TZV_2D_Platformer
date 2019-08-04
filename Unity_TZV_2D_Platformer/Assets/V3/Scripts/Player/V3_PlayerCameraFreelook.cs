using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3_PlayerCameraFreelook : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public V3_PlayerInput playerInput;
    public float horizontalFreelookOffset = 5f;
    public float verticalFreelookOffset = 5f;

    //****************************************************************************************************
    private void Update()
    {
        transform.position = new Vector3
            (
                playerTransform.position.x + offset.x + (horizontalFreelookOffset * playerInput.freelookHorizontal),
                playerTransform.position.y + offset.y + (verticalFreelookOffset * playerInput.freelookVertical),
                playerTransform.position.z + offset.z
            );

    }
}
