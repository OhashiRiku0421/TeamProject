using UnityEngine;

public class RestOfNeed : MonoBehaviour, IDamage
{
    [SerializeField, Tooltip("ó^É_ÉÅ")]
    private int _damage = 100;
    [SerializeField, Tooltip("äÓñ{HP")]
    private float _baseHP = 1000F;

    private float _hp;
    private StateMachine _stateMachine;

    private void Start()
    {
        _hp = _baseHP;
        _stateMachine = new StateMachine();
        _stateMachine.ChangeState(AliveState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_stateMachine.CurrentState == DeadState)
            return;

        if (other.gameObject.TryGetComponent<IDamage>(out IDamage iDamage))
        {
            CriAudioManager.Instance.SE.Play("Enemy", "SE_Enemy_Hit_Bad");
            iDamage.SendDamage(_damage);

            if (_stateMachine.CurrentState == AliveState)
            {
                // Transition to Dead state
                _stateMachine.ChangeState(DeadState);
            }
        }
    }

    private void AliveState()
    {
        // Handle logic for the Alive state
    }

    private void DeadState()
    {
        // Handle logic for the Dead state
        Destroy(gameObject);
        Debug.Log("Break");
    }

    public void SendDamage(float damage)
    {
        if (_stateMachine.CurrentState == DeadState)
            return;

        _hp -= damage;

        if (_hp <= 0)
        {
            // Transition to Dead state
            _stateMachine.ChangeState(DeadState);
        }
    }

    private class StateMachine
    {
        public delegate void State();

        public State CurrentState { get; private set; }

        public void ChangeState(State newState)
        {
            CurrentState = newState;
        }
    }
}
