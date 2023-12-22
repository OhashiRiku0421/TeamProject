using UnityEngine;
using System;

[Serializable]
public class EnemyStateMachine
{
    private IState _state;

    [SerializeField]
    private EnemyShortAttackState _shortAttack = new();

    [SerializeField]
    private EnemyLongAttackState _longAttack = new();

    [SerializeField]
    private EnemyMoveState _move = new();

    [SerializeField]
    private EnemyIdleState _idle = new();

    [SerializeField]
    private EnemyDamageState _damage = new();

    [SerializeField]
    private EnemyPatrolState _patrol = new();

    [SerializeField]
    private EnemyDeathState _death = new();

    public EnemyShortAttackState ShortAttack => _shortAttack;

    public EnemyLongAttackState LongAttack => _longAttack;

    public EnemyMoveState Move => _move;

    public EnemyIdleState Idle => _idle;

    public EnemyDamageState Damage => _damage;

    public EnemyPatrolState Patrol => _patrol;

    public EnemyDeathState Death => _death;

    public void Set(EnemyController enemyController)
    {
        _shortAttack.SetEnemy(enemyController);
        _longAttack.SetEnemy(enemyController);
        _move.SetEnemy(enemyController);
        _idle.SetEnemy(enemyController);
        _damage.SetEnemy(enemyController);
        _patrol.SetEnemy(enemyController);
        _death.SetEnemy(enemyController);
    }

    /// <summary>
    /// Stateの初期化
    /// </summary>
    public void StartState(IState state)
    {
        _state = state;
        state.Enter();
    }

    /// <summary>
    /// ステートが変わる時に実行する
    /// </summary>
    public void ChangeState(IState nextState)
    {
        _state.Exit();
        _state = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        _state.Update();
    }
}
