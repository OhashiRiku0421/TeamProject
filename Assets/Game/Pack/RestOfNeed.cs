using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestOfNeed : MonoBehaviour, IDamage
{
    [SerializeField, Tooltip("与ダメ")]
    private int _damage = 100;
    [SerializeField, Tooltip("基本HP")]
    private float _baseHP = 1000F;
    [SerializeField, Tooltip("パーティクル入れる場所")]
    // Reference to the particle system prefab
    public GameObject particleSystemPrefab;

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
        Debug.Log("attacked");

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
    void OnDestroy()
    {
        // Instantiate or activate the particle system when the object is destroyed
        if (particleSystemPrefab != null)
        {
            Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            Debug.Log("play");
        }
    }
}
