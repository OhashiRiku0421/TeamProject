using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

[Serializable]
public class EnemyAttack
{
    [SerializeField, Tooltip("攻撃のインターバル")]
    private float _awaitAttack = 3;

    [SerializeField, Tooltip("攻撃力")]
    private float _attackPower = 1;

    [SerializeField]
    private GameObject _effect;

    [Header("遠距離攻撃")]

    [SerializeField, Tooltip("遠距離攻撃の範囲")]
    private float _longAttackRange = 10f;

    [Header("近距離攻撃")]

    [SerializeField, Tooltip("近距離攻撃の当たり判定のサイズ")]
    private Vector3 _shortAttackSize;

    [SerializeField, Tooltip("近距離攻撃の当たり判定の中心")]
    private Vector3 _shortAttackCenter;

    [SerializeField]
    private BulletController _bullet;

    [SerializeField]
    private Transform _muzzel;

    private Transform _transform;

    private Transform _player;

    private Animator _anim;

    private bool _isAttack = false;

    private bool _isCancel = false;

    private CancellationTokenSource _cancell = new();

    public bool IsAttack { get => _isAttack; set => _isAttack = value; }

    public bool IsCancel { get => _isCancel; set => _isCancel = value; }

    public float LongAttackRange => _longAttackRange;

    public Vector3 ShortAttackSize => _shortAttackSize;

    public Vector3 ShortAttackCenter => _shortAttackCenter;

    public void Init(Transform transform, Transform player, Animator anim)
    {
        _transform = transform;
        _player = player;
        _anim = anim;
    }

    /// <summary>
    /// 遠距離攻撃
    /// </summary>
    public void LongAttack()
    {
        BulletController bullet = UnityEngine.Object.Instantiate(_bullet, _muzzel.position, Quaternion.identity);
        bullet.BulletShot(_transform.forward);
        CriAudioManager.Instance.SE.Play("SE", "SE_Enemy02_Attack_01");
        _isCancel = true;
    }

    /// <summary>
    /// 近距離攻撃
    /// </summary>
    public void ShortAttck()
    {
        //攻撃範囲
        Collider[] colliders = Physics.OverlapBox(_transform.TransformPoint(_shortAttackCenter),
        _shortAttackSize * 0.5f, _transform.rotation);
        //範囲に入ったら攻撃
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<IDamage>(out IDamage damage))
            {
                GameObject.Instantiate(_effect, collider.gameObject.transform.position, Quaternion.identity);
                CriAudioManager.Instance.SE.Play("SE", "SE_Enemy01_Attack_01");
                damage.SendDamage(_attackPower);
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
        await UniTask.Delay(TimeSpan.FromSeconds(_awaitAttack), cancellationToken: _cancell.Token);
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
