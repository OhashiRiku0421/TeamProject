using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_1 : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    private int _damage = 10;

    private enum MovementState
    {
        Moving,
        Paused
    }

    private MovementState currentState = MovementState.Moving;
    private int waypointIndex = 0;
    private int movementDirection = 1;
    private int i = 1;

    void Update()
    {
        switch (currentState)
        {
            case MovementState.Moving:
                Move();
                break;
            case MovementState.Paused:
                // Handle logic when movement is paused, if needed
                break;
        }
    }

    void Move()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned.  行き場が無い");
            return;
        }

        Transform currentWaypoint = waypoints[waypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

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
