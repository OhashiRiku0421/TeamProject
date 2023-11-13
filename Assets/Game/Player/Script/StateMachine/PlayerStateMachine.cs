using System;
using System.Collections;
using System.Collections.Generic;
using CustomStateMachine;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveController))]
public class PlayerStateMachine : AbstractStateMachineBase
{
    /// <summary>ステートデータをキャッシュしておく</summary>
    private Dictionary<string, AbstractStateBase> _stateCache = new Dictionary<string, AbstractStateBase>();

    /// <summary>PlayerMoveController</summary>
    public PlayerMoveController MoveController { get; private set; }

    /// <summary>PlayerAttackController</summary>
    public PlayerAttackController AttackController { get; private set; }
    
    /// <summary>PlayerCollectController</summary>
    public PlayerCollectController CollectController { get; private set; }
    
    /// <summary>AvoidController</summary>
    public PlayerAvoidController AvoidController { get; private set; }

    private void Awake()
    {
        // Cacheを作っておく
        _stateCache.Add(IdleState.STATE_NAME, new IdleState(this));
        _stateCache.Add(MoveState.STATE_NAME, new MoveState(this));
        _stateCache.Add(CollectingState.STATE_NAME, new CollectingState(this));
        _stateCache.Add(AvoidanceState.STATE_NAME, new AvoidanceState(this));
        _stateCache.Add(AttackState.STATE_NAME, new AttackState(this));
        _stateCache.Add(HitDamageState.STATE_NAME, new HitDamageState(this));
        _stateCache.Add(JumpState.STATE_NAME, new JumpState(this));

        // 参照キャッシュ
        MoveController = GetComponent<PlayerMoveController>();
        AttackController = GetComponent<PlayerAttackController>();
        CollectController = GetComponent<PlayerCollectController>();
        AvoidController = GetComponent<PlayerAvoidController>();
    }

    protected override AbstractStateBase StateInit()
    {
        // 初期ステートを指定
        return _stateCache[IdleState.STATE_NAME];
    }

    public override void TransitionTo(string nextStateName)
    {
        CurrentState.OnExit();
        CurrentState = _stateCache[nextStateName];
        CurrentState.OnEntry();

        Debug.Log($"現在のステートは{CurrentState.StateName}");

        // Actionを呼び出す
        _onStateChanged?.Invoke(CurrentState);
    }
}