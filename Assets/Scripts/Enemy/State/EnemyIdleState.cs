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
        _enemy.Anim.SetBool("IsPatrol", false);
    }

    public void Update()
    {
        if(_enemy.EnemyMove.IsMove())
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.Move);
        }
    }

    public void Exit() 
    {
        if (_enemy.EnemyType == EnemyType.Short)
        {
            CriAudioManager.Instance.SE.Play3D(_enemy.transform.position, "SE", "SE_Enemy01_Voice_01");
        }
        else
        {
            CriAudioManager.Instance.SE.Play3D(_enemy.transform.position, "SE", "SE_Enemy02_Voice_01");
        }
    }


}
