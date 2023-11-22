using UnityEngine;

/// <summary>
/// 移動のステート
/// </summary>
public class EnemyMoveState : IState
{

    private EnemyController _enemy;


    public void SetEnemy(EnemyController enemyController)
    {
        _enemy = enemyController;
    }

    public void Enter()
    {
        Debug.Log("Move");
        _enemy.Anim.SetBool("IsMove", true);
    }

    public void Update()
    {
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.PlayerTransform.position);
        _enemy.EnemyMove.Rotation();
        _enemy.EnemyMove.Move();

        if (distance <= _enemy.EnemyMove.StopDistance)
        {
            if (_enemy.EnemyType == EnemyType.Short)
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.ShortAttack);
            }
            else
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.LongAttack);
            }
            return;
        }

        //Playerが移動範囲外に出たらIdelステートに変更する
        if (distance > _enemy.EnemyMove.MoveDistance)
        {
            if(_enemy.IdleType == IdleType.Normal)
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.Idle);
            }
            else
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.Patrol);
            }
        }
    }

    public void Exit()
    {
        _enemy.Anim.SetBool("IsMove", false);
        _enemy.EnemyMove.MoveStop();
    }
}
