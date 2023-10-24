using UnityEngine;

/// <summary>
/// Idel状態のステート
/// </summary>
[System.Serializable]
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
    }

    public void Update()
    {
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.PlayerTransform.position);

        //移動範囲内に入ったら移動のStateに変更
        if (distance <= _enemy.MoveDistance)
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.Move);
        }
    }

    public void Exit() { }


}
