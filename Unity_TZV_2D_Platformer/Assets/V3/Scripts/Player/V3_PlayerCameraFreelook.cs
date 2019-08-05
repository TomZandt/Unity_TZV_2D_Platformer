using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V3_PlayerCameraFreelook : MonoBehaviour
{
    public V3_SO_Input inputSO;
    public Transform playerTransform;
    public Vector3 offset;
    public V3_PlayerInput playerInput;
    public float horizontalFreelookOffset = 5f;
    public float verticalFreelookOffset = 5f;

    //****************************************************************************************************
    private void Awake()
    {
        if (inputSO == null)
        {
            Debug.LogError("No SO Found - TZV V3_PlayerCameraFreelook");
        }
    }

    //****************************************************************************************************
    private void Update()
    {
        transform.position = new Vector3
            (
                playerTransform.position.x + offset.x + (horizontalFreelookOffset * inputSO.freelookHorizontal),
                playerTransform.position.y + offset.y + (verticalFreelookOffset * inputSO.freelookVertical),
                playerTransform.position.z + offset.z
            );

    }
}
