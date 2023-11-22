using UnityEngine;

public class SnakeSegment : MonoBehaviour
{
    [SerializeField, Tooltip("パトロールする場所")]
    public Transform target; // The target to follow
    [SerializeField, Tooltip("付いていく距離")]
    public float followDistance = 0.5f; // The distance between body segments
    [SerializeField, Tooltip("付いていくスピード")]
    public float followSpeed = 5.0f; // The speed of the follower

    private StateMachine _stateMachine;
    private bool _isPaused = false;

    void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine.ChangeState(FollowState);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            TogglePauseResume();
        }

        if (!_isPaused)
        {
            _stateMachine.UpdateState();
        }
    }

    private void FollowState()
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

    private void TogglePauseResume()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            _stateMachine.ChangeState(PauseState);
        }
        else
        {
            _stateMachine.ChangeState(FollowState);
        }
    }

    private void PauseState()
    {
        // Handle logic for the pause state, if needed
    }

    private class StateMachine
    {
        public delegate void State();

        private State _currentState;

        public void ChangeState(State newState)
        {
            _currentState = newState;
        }

        public void UpdateState()
        {
            _currentState?.Invoke();
        }
    }
}
