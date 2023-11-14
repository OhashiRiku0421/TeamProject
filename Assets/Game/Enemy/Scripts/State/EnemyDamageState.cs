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
        _enemy.Anim.SetTrigger("IsHit");
        CriAudioManager.Instance.SE.Play("SE", "SE_EnemyAll_Damage");
    }

    public void Update()
    {
        if (!_enemy.Anim.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
        {
            if (_enemy.EnemyType == EnemyType.Short)
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.ShortAttack);
            }
            else
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.LongAttack);
            }
        }
    }

    public void Exit() 
    {
    }
}
