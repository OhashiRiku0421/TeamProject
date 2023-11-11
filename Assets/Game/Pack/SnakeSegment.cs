using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    public Transform target; // The target to follow
    public float followDistance = 0.5f; // The distance between body segments
    public float followSpeed = 5.0f; // The speed of the follower

    void Update()
    {
        if (target != null)
        {
            // Move the segment towards the target position with smoothing
            transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

            // Calculate the direction to the target without considering the Y-axis
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0;

            // Rotate the segment to align with the direction of movement
            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget.normalized, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
            }
        }
    }
}
