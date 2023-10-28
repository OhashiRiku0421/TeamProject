using UnityEngine;

/// <summary>
/// 移動のステート
/// </summary>
[System.Serializable]
public class EnemyMoveState : IState
{
    [SerializeField, Tooltip("移動のスピード")]
    private float _moveSpeed = 5;

    private EnemyController _enemy;


    public void SetEnemy(EnemyController enemyController)
    {
        _enemy = enemyController;
    }

    public void Enter()
    {
        Debug.Log("Move");
    }

    public void Update()
    {
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.PlayerTransform.position);
        //向き
        _enemy.transform.LookAt(_enemy.PlayerTransform);
        //移動
        _enemy.Rb.velocity = (_enemy.PlayerTransform.position - _enemy.transform.position).normalized * _moveSpeed;
        //近くに来たら攻撃ステートに変更する
        if (distance <= _enemy.StopDistance)
        {
            if (_enemy.EnemyType == EnemyType.Short)
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.ShortAttack);
            }
            else
            {
                Debug.Log(distance);
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.LongAttack);
            }
            return;
        }
        //Playerが移動範囲外に出たらIdelステートに変更する
        if (distance > _enemy.MoveDistance)
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.Idle);
        }
    }

    public void Exit() 
    {
        _enemy.Rb.velocity = Vector3.zero;
    }
}
