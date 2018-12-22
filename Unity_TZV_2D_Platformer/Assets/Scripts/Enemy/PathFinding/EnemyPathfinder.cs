//****************************************************************************************************
// Ref: https://arongranberg.com/astar/docs/custom_movement_script.html
// Ref: https://arongranberg.com/astar/docs/astaraics.html
//****************************************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(SimpleSmoothModifier))]

public class EnemyPathfinder : MonoBehaviour
{
    public Vector2 targetPosition;
    public EnemyStats enemyStats;

    private Seeker seeker;
    private Path path;
    private int currentWaypoint;
    private bool reachedEndOfPath;

    public float repathRate = 0.5f;
    private float lastRepath = float.NegativeInfinity;

    private Vector3 directionToReturn;

    //****************************************************************************************************
    private void Start()
    {
        directionToReturn = new Vector2(0f, 0f);

        seeker = GetComponent<Seeker>();

        seeker.StartPath(transform.position, enemyStats.target, OnPathComplete);
    }

    //****************************************************************************************************
    private void OnPathComplete(Path p)
    {
        p.Claim(this);

        if (p.error)
        {
            Debug.Log(p.errorLog);

            p.Release(this);
        }
        else
        {
            if (path != null) path.Release(this);

            path = p;

            currentWaypoint = 0;
        }
    }

    //****************************************************************************************************
    public void OnDisable()
    {
        seeker.pathCallback -= OnPathComplete; // Deregister callback
    }

    //****************************************************************************************************
    private void Update()
    {
        if (Time.time > lastRepath + repathRate && seeker.IsDone())
        {
            lastRepath = Time.time;

            seeker.StartPath(transform.position, enemyStats.target, OnPathComplete);
        }

        // Dont do anything if we have no path
        if (path == null) return;

        reachedEndOfPath = false;

        float distanceToWaypoint;

        while (true)
        {
            distanceToWaypoint = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

            if (distanceToWaypoint < enemyStats.waypointCompleteDistance)
            {
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        directionToReturn = direction;
    }

    //****************************************************************************************************
    public Vector3 getDirection()
    {
        return directionToReturn;
    }
}
