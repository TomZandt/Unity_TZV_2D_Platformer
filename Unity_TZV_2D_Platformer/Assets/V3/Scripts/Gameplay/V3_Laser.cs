using UnityEngine;

public class V3_Laser : MonoBehaviour
{
    public float rotateSpeed = 3f;
    public bool canRotate = true;
    public bool rotateClockwise = true;

    //****************************************************************************************************
    private void Update()
    {
        if (canRotate && rotateClockwise)
            transform.Rotate(0f, 0f, -rotateSpeed * 10 * Time.deltaTime);
        else if (canRotate && !rotateClockwise)
            transform.Rotate(0f, 0f, rotateSpeed * 10 * Time.deltaTime);
    }
}
