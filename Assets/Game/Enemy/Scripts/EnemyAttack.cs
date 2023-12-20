using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

[Serializable]
public class EnemyAttack
{
    [SerializeField]
    private GameObject _effect;

    [SerializeField]
    private BulletController _bullet;

    [SerializeField]
    private Transform _muzzel;

    private Transform _transform;

    private Animator _anim;

    private bool _isAttack = false;

    private bool _isCancel = false;

    private CancellationTokenSource _cancell = new();

    private EnemyData _data;

    public bool IsAttack { get => _isAttack; set => _isAttack = value; }

    public bool IsCancel { get => _isCancel; set => _isCancel = value; }

    public void Init(Transform transform, Animator anim, EnemyData data)
    {
        _transform = transform;
        _anim = anim;
        _data = data;
    }

    /// <summary>
    /// 遠距離攻撃
    /// </summary>
    public void LongAttack()
    {
        BulletController bullet = UnityEngine.Object.Instantiate(_bullet, _muzzel.position, Quaternion.identity);
        bullet.BulletShot(_transform.forward);
        CriAudioManager.Instance.SE.Play3D(_transform.position, "SE", "SE_Enemy02_Voice_02");
        CriAudioManager.Instance.SE.Play3D(_transform.position, "SE", "SE_Enemy02_Attack_01");
        _isCancel = true;
    }

    /// <summary>
    /// 近距離攻撃
    /// </summary>
    public void ShortAttck()
    {
        //攻撃範囲
        Collider[] colliders = Physics.OverlapBox(_transform.TransformPoint(_data.ShortAttackCenter),
        _data.ShortAttackSize * 0.5f, _transform.rotation);
        CriAudioManager.Instance.SE.Play3D(_transform.position, "SE", "SE_Enemy01_Voice_02");
        CriAudioManager.Instance.SE.Play3D(_transform.position, "SE", "SE_Enemy01_Attack_01");
        //範囲に入ったら攻撃
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<IDamage>(out IDamage damage))
            {
                GameObject.Instantiate(_effect, collider.gameObject.transform.position, Quaternion.identity);
                damage.SendDamage(_data.AttackPower);
                _isCancel = true;
            }
        }
    }

    /// <summary>
    /// 攻撃を待つ
    /// </summary>
    public async UniTask AttackAsync()
    {
        _isAttack = true;
        _isCancel = false;
        //攻撃の時間
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: _cancell.Token);
        await IntervalAsync();
    }

    public async UniTask IntervalAsync()
    {
        _anim.SetBool("IsShortAttack", false);
        _anim.SetBool("IsLongAttack", false);
        _isAttack = true;
        _isCancel = true;
        //攻撃のインターバル
        await UniTask.Delay(TimeSpan.FromSeconds(_data.AwaitAttack), cancellationToken: _cancell.Token);
        _isAttack = false;
    }

    public void AttackExit()
    {
        _cancell?.Cancel();
        _cancell = new();
        _isAttack = false;
        _isCancel = false;
    }
}
