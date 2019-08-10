using System.Collections;
using UnityEngine;

public class V3_FallingBlock : MonoBehaviour
{
    public float fallDelay = 0f;
    public bool destroyAfterFall = false;

    private Rigidbody2D rb2D;

    private bool hasFallen = false;
    private int platformLayer;

    //****************************************************************************************************
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        rb2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb2D.useFullKinematicContacts = true;

        // Get the integer representation
        platformLayer = LayerMask.NameToLayer("Platform");

        hasFallen = false;

        gameObject.layer = LayerMask.NameToLayer("Trap");
    }

    //****************************************************************************************************
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == platformLayer && hasFallen)
        {
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            gameObject.layer = platformLayer;
        }
    }

    //****************************************************************************************************
    public IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        hasFallen = true;

        if (destroyAfterFall)
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}
