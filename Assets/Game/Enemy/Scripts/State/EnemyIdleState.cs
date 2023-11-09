using UnityEngine;

/// <summary>
/// Idel状態のステート
/// </summary>
public class EnemyIdleState : IState
{
    private EnemyController _enemy;

    public void SetEnemy(EnemyController enemyController)
    {
        _enemy = enemyController;
    }

    public void Enter()
    {
        Debug.Log("Idle");
        _enemy.Anim.SetBool("IsPatrol", false);
    }

    public void Update()
    {
        if(_enemy.EnemyMove.IsMove())
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.Move);
        }
    }

    public void Exit() { }


}
