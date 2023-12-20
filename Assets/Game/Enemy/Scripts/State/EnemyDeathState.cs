using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : IState
{
    private EnemyController _enemy;

    public void SetEnemy(EnemyController enemyController)
    {
        _enemy = enemyController;
    }

    public void Enter()
    {
        _enemy.Anim.SetTrigger("Death");
        CriAudioManager.Instance.SE.Play3D(_enemy.gameObject.transform.position
            , "SE", "SE_EnemyAll_Dead");
        if (_enemy.EnemyType == EnemyType.Short)
        {
            CriAudioManager.Instance.SE.Play3D(_enemy.gameObject.transform.position,
                "SE", "SE_Enemy01_Voice_04");
        }
        else
        {
            CriAudioManager.Instance.SE.Play3D(_enemy.gameObject.transform.position,
                "SE", "SE_Enemy02_Voice_04");
        }
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}
