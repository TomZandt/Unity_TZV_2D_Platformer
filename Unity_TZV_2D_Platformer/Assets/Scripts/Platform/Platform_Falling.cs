using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Falling : MonoBehaviour
{
    [SerializeField] private float fallDelay = 2f;
    private Rigidbody2D rb2D;

    private float refVel;
    float currentRot;

    bool canWiggle = false;

    //****************************************************************************************************
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        rb2D.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        if (canWiggle == true)
        {
            StartCoroutine(Wiggle());
        }
    }

    //****************************************************************************************************
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            canWiggle = true;

            StartCoroutine(Fall(fallDelay));
        }
    }

    //****************************************************************************************************
    IEnumerator Fall(float _seconds)
    {
        yield return new WaitForSeconds(fallDelay);
        rb2D.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    //****************************************************************************************************
    IEnumerator Wiggle()
    {
        currentRot = rb2D.rotation;

        float temp = Mathf.SmoothDampAngle(currentRot, Mathf.Sin(Random.Range(1, 10) * 10), ref refVel, 0f);

        rb2D.SetRotation(temp);

        yield return new WaitForSeconds(0f);
    }
}
