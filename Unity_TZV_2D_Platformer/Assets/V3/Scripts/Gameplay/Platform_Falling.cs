using System.Collections;
using UnityEngine;

public class Platform_Falling : MonoBehaviour
{
    public float fallDelay = 2f;
    public bool destroyAfterFall = false;

    private Rigidbody2D rb2D;
    private float refVel;
    private float currentRot;
    private bool hasFallen = false;
    private int playerLayer;

    //****************************************************************************************************
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        rb2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb2D.useFullKinematicContacts = true;

        // Get the integer representation
        playerLayer = LayerMask.NameToLayer("Player");

        hasFallen = false;
    }

    //****************************************************************************************************
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer && !hasFallen)
        {
            hasFallen = true;
            StartCoroutine(Fall());
        }
    }

    //****************************************************************************************************
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        rb2D.mass = 10f;

        if (destroyAfterFall)
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}
