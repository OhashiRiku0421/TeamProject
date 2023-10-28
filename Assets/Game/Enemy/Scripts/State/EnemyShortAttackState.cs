using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

/// <summary>
/// 近距離攻撃のステート
/// </summary>
[Serializable]
public class EnemyShortAttackState : IState
{
    [SerializeField, Tooltip("攻撃のインターバル")]
    private float _awaitAttack = 3;

    [SerializeField, Tooltip("攻撃力")]
    private float _attackPower = 1;
    [SerializeField]
    private GameObject _effect;

    private bool _isAttack = false;
    private bool _isCancel = false;
    private EnemyController _enemy;

    private CancellationTokenSource _cancell = new();

    public void SetEnemy(EnemyController enemyController)
    {
        _enemy = enemyController;
    }

    public void Enter()
    {
        IntervalAsync();
    }

    public void Update()
    {
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.PlayerTransform.position);
        if (!_isAttack)
        {
            _enemy.transform.LookAt(_enemy.PlayerTransform);
            AttackAsync();
        }
        else if (!_isCancel)
        {
            //攻撃範囲
            Collider[] colliders = Physics.OverlapBox(_enemy.transform.TransformPoint(_enemy.AttackCenter),
            _enemy.AttackSize * 0.5f, _enemy.transform.rotation);
            //範囲に入ったら攻撃
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<IDamage>(out IDamage damage))
                {
                    GameObject.Instantiate(_effect, collider.gameObject.transform.position, Quaternion.identity);
                    damage.SendDamage(_attackPower);
                    _isCancel = true;
                }
            }
        }
        //ちょっと遠くなったら移動のStateに変更
        if (distance >= _enemy.StopDistance + 2)//仮
        {
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.Move);
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private async UniTask AttackAsync()
    {
        _isAttack = true;
        _isCancel = false;
        await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: _cancell.Token);//攻撃の時間
        await IntervalAsync();
    }

    private async UniTask IntervalAsync()
    {
        _isAttack = true;
        _isCancel = true;
        await UniTask.Delay(TimeSpan.FromSeconds(_awaitAttack), cancellationToken: _cancell.Token);//攻撃のインターバル
        _isCancel = false;
        _isAttack = false;
    }

    public void Exit()
    {
        _isAttack = false;
        _isCancel = false;
    }
}
