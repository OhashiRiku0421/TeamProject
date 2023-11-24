using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_1 : MonoBehaviour
{
    [SerializeField, Tooltip("行く場所")]
    public Transform[] waypoints;
    [SerializeField, Tooltip("動くスピード")]
    public float moveSpeed = 2f;

    private int waypointIndex = 0;
    private int movementDirection = 1;
    private int i = 1;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            isPaused = !isPaused;
        }

        if (!isPaused)
        {
            Move();
        }
    }

    void Move()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned. 行き場が無い");
            return;
        }

        Transform currentWaypoint = waypoints[waypointIndex];

        // Calculate the direction to the current waypoint
        Vector3 directionToWaypoint = currentWaypoint.position - transform.position;

        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        // Rotate towards the waypoint without changing the base orientation
        if (directionToWaypoint != Vector3.zero)
        {
            Quaternion rotationToWaypoint = Quaternion.LookRotation(directionToWaypoint, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToWaypoint, Time.deltaTime * 5f);
        }

        if (transform.position == currentWaypoint.position)
        {
            waypointIndex += movementDirection;

            if (waypointIndex >= waypoints.Length || waypointIndex < 0)
            {
                // スタートと終わりにカウントする
                movementDirection *= -1;
                waypointIndex += 2 * movementDirection;
                Debug.Log(i++);
            }
        }
    }
}

