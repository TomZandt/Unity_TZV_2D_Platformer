using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Platform_SequenceWaypoint : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    public float platformSpeed;

    private bool goToWP1 = true;
    private bool goToWP2 = false;

    private Rigidbody2D rb2D;

    //****************************************************************************************************
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        rb2D.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    //****************************************************************************************************
    void FixedUpdate()
    {
        // If we are not at waypoint 1 and we are meant to be...
        if (transform.position != waypoint1.position && goToWP1 == true)
        {
            // Go to waypoint 1
            transform.position = Vector3.MoveTowards(transform.position, waypoint1.position, platformSpeed / 100f);
            //rb2D.MovePosition(Vector2.MoveTowards(transform.position, waypoint1.position, platformSpeed / 100f));
        }
        else
        {
            goToWP1 = false;
            goToWP2 = true;
        }

        // If we are not at waypoint 2 and we are meant to be...
        if (transform.position != waypoint2.position && goToWP2 == true)
        {
            // Go to waypoint 2
            transform.position = Vector3.MoveTowards(transform.position, waypoint2.position, platformSpeed / 100f);
            //rb2D.MovePosition(Vector2.MoveTowards(transform.position, waypoint2.position, platformSpeed / 100f));
        }
        else
        {
            goToWP1 = true;
            goToWP2 = false;
        }
    }

    //****************************************************************************************************
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && collision.transform.parent != this.transform)
        {
            collision.transform.SetParent(this.transform);
        }
    }

    //****************************************************************************************************
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
