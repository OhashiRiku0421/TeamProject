using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IState
{

    private EnemyController _enemy;

    public void SetEnemy(EnemyController enemyController)
    {
        _enemy = enemyController;
    }

    public void Enter() 
    {
        _enemy.Anim.SetBool("IsPatrol", true);
        _enemy.Agent.autoBraking = false;
        _enemy.EnemyMove.Patrol();
    }

    public void Update()
    {
        if (!_enemy.Agent.pathPending && _enemy.Agent.remainingDistance < 0.5f)
        {
            //移動地点を更新
            _enemy.EnemyMove.Patrol();
        }

        if(_enemy.EnemyMove.IsMove())
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.Move);
        }
    }

    public void Exit() 
    {
        CriAudioManager.Instance.SE.Play3D(_enemy.transform.position, "SE", "SE_Enemy01_Voice_01");
    }
}
