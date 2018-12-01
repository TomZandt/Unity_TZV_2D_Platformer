using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Bash : MonoBehaviour
{
    public float bashTriggerRadius = 2f;
    public float bashSpeed = 20f;
    public float bashOffsetRadiusMultiplier = 1.5f;
    public float bashPauseTimeSeconds = 1f;
    public Transform arrowImage;
    public float arrowYOffset = 0.7f;

    RaycastHit2D[] nearbyObjects;
    Vector3 bashDirection;
    GameObject bashableObject;
    bool canBash = false;
    bool bashNow = false;

    void Start()
    {
        arrowImage.gameObject.SetActive(false);
    }
    //******************************************************************************
    // Update is called once per frame
    //******************************************************************************
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            nearbyObjects = Physics2D.CircleCastAll(transform.position, bashTriggerRadius, Vector3.forward);
            {
                foreach (RaycastHit2D obj in nearbyObjects)
                {
                    if (obj.collider.gameObject.GetComponent<bashable>() != null)
                    {
                        bashableObject = obj.collider.gameObject;
                        StartCoroutine("BashPauseCounter");

                        SetGameTime(0);
                        canBash = true;

                        arrowImage.position = bashableObject.transform.position;
                        arrowImage.Translate(0f, 0.7f, 10f);
                        arrowImage.gameObject.SetActive(true);

                        break;
                    }
                }
            }
        }
        else if (Input.GetButtonUp("Fire2") && canBash)
        {
            SetGameTime(1);

            bashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - bashableObject.transform.position;
            bashDirection = bashDirection.normalized;
            bashDirection.z = 0f;

            transform.position = bashableObject.transform.position + (bashDirection * 1.5f);

            canBash = false;
            bashNow = true;
            arrowImage.gameObject.SetActive(false);
        }
        else if (Input.GetButton("Fire2") && canBash)
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            arrowImage.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90f);
        }
    }

    //******************************************************************************
    // Called every physics step
    //******************************************************************************
    void FixedUpdate()
    {
        if (bashNow)
        {
            GetComponent<Rigidbody2D>().velocity = bashDirection * bashSpeed;
            bashNow = false;
        }
    }
    //******************************************************************************
    // Freeze Time
    //******************************************************************************
    void SetGameTime(float newTime)
    {
        Time.timeScale = newTime;
    }

    IEnumerator BashPauseCounter()
    {
        float pauseTime = Time.realtimeSinceStartup + bashPauseTimeSeconds;

        while (Time.realtimeSinceStartup < pauseTime)
        {
            yield return null;
        }

        if (Time.timeScale == 0)
        {
            SetGameTime(1);
            canBash = false;
            bashNow = false;
            arrowImage.gameObject.SetActive(false);
        }
    }
}
/**

//******************************************************************************
// Reset position
//******************************************************************************
void resetPosition()
{
    float width = bashObject.GetComponent<SpriteRenderer>().bounds.size.x;
    float height = bashObject.GetComponent<SpriteRenderer>().bounds.size.y;

    float tan = Mathf.Atan2(bashDirection.y, bashDirection.x);

    int roundCos = Mathf.RoundToInt(Mathf.Cos(tan));
    int roundSin = Mathf.RoundToInt(Mathf.Sin(tan));

    Vector3 newVect;
    newVect.x = bashObject.transform.position.x + (width * roundCos);
    newVect.y = bashObject.transform.position.y + (height * roundSin);
    newVect.z = 0;

    transform.position = newVect;
}
} */
