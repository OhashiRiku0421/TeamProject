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

    private void Awake()
    {
        // Cacheを作っておく
        _stateCache.Add(IdleState.STATE_NAME, new IdleState(this));
        _stateCache.Add(WalkState.STATE_NAME, new WalkState(this));
        _stateCache.Add(RunState.STATE_NAME, new RunState(this));
        _stateCache.Add(CloseRangeAttackState.STATE_NAME, new CloseRangeAttackState(this));
        _stateCache.Add(LongRangeAttackState.STATE_NAME, new CloseRangeAttackState(this));
        _stateCache.Add(HitDamageState.STATE_NAME, new HitDamageState(this));
        
        // 参照キャッシュ
        MoveController = GetComponent<PlayerMoveController>();
        AttackController = GetComponent<PlayerAttackController>();
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
        CurrentState.OnExit();
        
        Debug.Log(CurrentState);
        
        // Actionを呼び出す
        _onStateChanged?.Invoke(CurrentState);
    }
}
