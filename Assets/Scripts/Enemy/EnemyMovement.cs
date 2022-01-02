using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is here in case we need to implement other movements methods
public class EnemyMovement : MonoBehaviour
{
    public int MoveToNextWaypoint(float moveSpeed, GameObject[] waypoints, int currentWaypointTarget)
    {
        float step = moveSpeed * Time.deltaTime;
        if (currentWaypointTarget > (waypoints.Length - 1))
        {
            currentWaypointTarget = 0;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointTarget].transform.position, step);
        if (transform.position.x == waypoints[currentWaypointTarget].transform.position.x && transform.position.y == waypoints[currentWaypointTarget].transform.position.y)
        {
            currentWaypointTarget++;
        }
        return currentWaypointTarget;

    }
}
