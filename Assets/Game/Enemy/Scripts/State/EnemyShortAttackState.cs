using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 近距離攻撃のステート
/// </summary>
public class EnemyShortAttackState : IState
{

    private EnemyController _enemy;

    public void SetEnemy(EnemyController enemyController)
    {
        _enemy = enemyController;
    }

    public void Enter()
    {
        _enemy.Anim.SetBool("IsAttack", true);
        _enemy.EnemyAttack.IntervalAsync().Forget();
    }

    public void Update()
    {
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.PlayerTransform.position);

        _enemy.EnemyMove.Rotation();

        if (!_enemy.EnemyAttack.IsAttack)
        {
            _enemy.Anim.SetBool("IsShortAttack", true);
            _enemy.EnemyAttack.IsAttack = true;
        }
        else if (!_enemy.EnemyAttack.IsCancel)
        {
            _enemy.EnemyAttack.ShortAttck();
        }

        //ちょっと遠くなったら移動のStateに変更
        if (distance >= _enemy.Data.StopDistance + 2)//仮
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.Move);
        }
    }

    public void Exit()
    {
        _enemy.Anim.SetBool("IsAttack", false);
        _enemy.EnemyAttack.AttackExit();
    }
}
