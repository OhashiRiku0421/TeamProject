using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダメージのステート
/// </summary>
public class EnemyDamageState : IState
{
    private EnemyController _enemy;

    public void SetEnemy(EnemyController enemyController)
    {
        _enemy = enemyController;
    }

    public void Enter()
    {
        Debug.Log("Damage");
        //ダメージのAnimationを流す予定
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.PlayerTransform.position);
        //移動範囲に入っていたら移動のステートに変更
        if (distance > _enemy.MoveDistance)
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.Idle);
        }
    }

    public void Update()
    {
    }

    public void Exit() { }


}
