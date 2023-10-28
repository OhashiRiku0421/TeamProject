using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class PlayerAttackController : MonoBehaviour
{
    [FormerlySerializedAs("_normalAttackDamage")] [SerializeField, Tooltip("近距離攻撃の攻撃力")]
    private float _closeRangeAttackDamage = 10F;

    [FormerlySerializedAs("_normalAttackEreaCenter")] [SerializeField, Tooltip("近距離攻撃範囲の中心")]
    private Transform _closeAttackEreaCenter = default;

    [SerializeField, Tooltip("近距離攻撃")] private VisualEffect _closeRangeEffect = default;

    [SerializeField, Tooltip("遠距離攻撃のダメージ")]
    private float _longRangeDamage = 10F;
    
    [FormerlySerializedAs("_normalAttackHalfExtant")] [SerializeField, Tooltip("近距離攻撃の各軸に対する半径")]
    private Vector3 _closeRangeAttackHalfExtant = Vector3.zero;

    /// <summary>遠距離攻撃を発射する銃口</summary>
    [SerializeField]
    private GameObject _muzzle = default;
    
    [SerializeField, Tooltip("遠距離攻撃用のプレハブ")]
    private PlayerArrowController _longRangePrefab = default;

    /// <summary>現在近距離攻撃かどうか</summary>
    private bool _isCloseRange = true;

    /// <summary>現在近距離攻撃かどうか</summary>
    public bool IsCloseRange
    {
        get => _isCloseRange;
        private set
        {
            _onIsCloseRangeChanged?.Invoke(value);
            _isCloseRange = value;
        }
    }

    /// <summary>IsCloseRangeが変更された際に呼ばれるAction</summary>
    private Action<bool> _onIsCloseRangeChanged = default;
    
    /// <summary>IsCloseRangeが変更された際に呼ばれるAction</summary>
    public event Action<bool> OnIsCloseRangeChanged
    {
        add => _onIsCloseRangeChanged += value;
        remove => _onIsCloseRangeChanged -= value;
    }

    /// <summary>現在攻撃のAnimation中かどうか</summary>
    private bool _isAttackAnimation = false;

    /// <summary>現在攻撃のAnimation中かどうか</summary>
    public bool IsAttackAnimation
    {
        get => _isAttackAnimation;
        private set
        {
            _onIsAttackAnimationChanged?.Invoke(value);
            _isAttackAnimation = value;
        }
    }
    
    /// <summary>IsAttackAnimationが変更された際に呼ばれるAction</summary>
    private Action<bool> _onIsAttackAnimationChanged = default;
    
    /// <summary>IsAttackAnimationが変更された際に呼ばれるAction</summary>
    public event Action<bool> OnIsAttackAnimationChanged
    {
        add => _onIsAttackAnimationChanged += value;
        remove => _onIsAttackAnimationChanged -= value;
    }

    private void OnEnable()
    {
        CustomInputManager.Instance.PlayerInputActions.Player.AttackChange.started += AttackRangeChange;
        CustomInputManager.Instance.PlayerInputActions.Player.Attack.started += Attack;
    }

    private void OnDisable()
    {
        CustomInputManager.Instance.PlayerInputActions.Player.AttackChange.started -= AttackRangeChange;
        CustomInputManager.Instance.PlayerInputActions.Player.Attack.started -= Attack;
    }

    /// <summary>攻撃のタイプを変更する</summary>
    /// <param name="context">コールバック</param>
    private void AttackRangeChange(InputAction.CallbackContext context)
    {
        // Animation中は切り替えない
        if (IsAttackAnimation) return;
        
        // 近距離と遠距離を切り替える
        IsCloseRange = !IsCloseRange;
    }
    
    /// <summary>攻撃をする処理</summary>
    /// <param name="context">コールバック</param>
    private void Attack(InputAction.CallbackContext context)
    {
        // Animation中は攻撃できない
        if (IsAttackAnimation) return;
        
        IsAttackAnimation = true;

        if (!IsCloseRange)
        {
            NormalAttack();
        }
    }

    private void AttackEnd()
    {
        IsAttackAnimation = false;
    }

    /// <summary>通常攻撃の当たり判定をとる</summary>
    public void NormalAttack()
    {
        if (IsCloseRange)
        {
            CriAudioManager.Instance.SE.Play("Player", "SE_Player_Attack_Close");
            _closeRangeEffect.SendEvent("OnPlay");
            var colliders = Physics.OverlapBox(_closeAttackEreaCenter.position, _closeRangeAttackHalfExtant,
                this.transform.rotation);

            List<IDamage> targets = new List<IDamage>(colliders.Length);

            // とってきたものからダメージを与えられるものを探す
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IDamage target))
                {
                    targets.Add(target);
                    Debug.Log(target);
                }
            }

            Attack(targets, _closeRangeAttackDamage);
        }
        else
        {
            CriAudioManager.Instance.SE.Play("Player", "SE_Player_Attack_Far");
            var temp = Instantiate(_longRangePrefab.gameObject);
            temp.transform.position = _muzzle.transform.position;
            temp.transform.rotation = this.transform.rotation;
            temp.GetComponent<PlayerArrowController>().Damage = _longRangeDamage;
        }
    }

    /// <summary>攻撃のダメージ処理を行う関数</summary>
    /// <param name="targets">IDamageを使用したターゲット</param>
    /// <param name="damage">targetsに与えるダメージ</param>
    private void Attack(List<IDamage> targets, float damage)
    {
        foreach (var target in targets)
        {
            target.SendDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        // 通常攻撃の当たり判定を可視化
        Gizmos.color = Color.magenta;
        Gizmos.matrix = _closeAttackEreaCenter.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, _closeRangeAttackHalfExtant * 2F);
    }
}
