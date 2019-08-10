using UnityEngine;

public class V3_FallingBlockTrigger : MonoBehaviour
{
    public V3_FallingBlock blockToFall;

    private int playerLayer;

    //****************************************************************************************************
    private void Start()
    {
        // Get the integer representation
        playerLayer = LayerMask.NameToLayer("Player");
    }

    //****************************************************************************************************
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
            StartCoroutine(blockToFall.Fall());
    }
}
