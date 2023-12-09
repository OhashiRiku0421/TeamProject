using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class PlayerAttackController : MonoBehaviour, IPause
{
    [FormerlySerializedAs("_normalAttackDamage")] [SerializeField, Tooltip("近距離攻撃の攻撃力")]
    private float _closeRangeAttackDamage = 10F;

    [FormerlySerializedAs("_normalAttackEreaCenter")] [SerializeField, Tooltip("近距離攻撃範囲の中心")]
    private Transform _closeAttackEreaCenter = default;

    [SerializeField, Tooltip("近距離攻撃")] private VisualEffect _closeRangeEffect = default;

    [FormerlySerializedAs("_normalAttackHalfExtant")] [SerializeField, Tooltip("近距離攻撃の各軸に対する半径")]
    private Vector3 _closeRangeAttackHalfExtant = Vector3.zero;

    [SerializeField, Tooltip("入力可能な先行入力の数")] private int _attackBufferedInput = 1;
    
    /// <summary>現在のアタック入力回数</summary>
    private int _currentAttackInput = 0;

    public int CurrentAttackInput 
    {
        get => _currentAttackInput;
        private set
        {
            if(value != _currentAttackInput)
            {
                _onCurrentAttackInputChanged?.Invoke(value);
                _currentAttackInput = value;
            }
        }
    }

    private Action<int> _onCurrentAttackInputChanged = default;
    
    public event Action<int> OnCurrentAttackInputChanged
    {
        add => _onCurrentAttackInputChanged += value;
        remove => _onCurrentAttackInputChanged -= value;
    }

    /// <summary>現在のアタックパターン</summary>
    private int _currentAttackPattern = 0;

    /// <summary>現在のアタックパターン</summary>
    public int CurrentAttackPattern => _currentAttackPattern;

    /// <summary>次のアタックパターンに進める</summary>
    private void NextAttackPattern()
    {
        _currentAttackPattern = (_currentAttackPattern + 1) % _attackBufferedInput;
        _onCurrentAttackPatternChanged?.Invoke(_currentAttackPattern);
    }

    private void ResetAttackPattern()
    {
        _currentAttackPattern = 0;
        _onCurrentAttackPatternChanged?.Invoke(_currentAttackPattern);
    }

    private Action<int> _onCurrentAttackPatternChanged = default;
    
    public event Action<int> OnCurrentAttackPatternChanged
    {
        add => _onCurrentAttackPatternChanged += value;
        remove => _onCurrentAttackPatternChanged -= value;
    }
    
    /// <summary>現在攻撃のAnimation中かどうか</summary>
    private bool _isAttackAnimation = false;

    /// <summary>現在攻撃のAnimation中かどうか</summary>
    public bool IsAttackAnimation
    {
        get => _isAttackAnimation;
        private set
        {
            if (_isAttackAnimation != value)
            {
                _onIsAttackAnimationChanged?.Invoke(value);
                _isAttackAnimation = value;
            }
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

    private bool _isPause = false;

    private void Start()
    {
        ResetAttackPattern();
    }

    private void OnEnable()
    {
        PauseSystem.Instance.Register(this);
        CustomInputManager.Instance.PlayerInputActions.Player.Attack.started += Attack;
    }

    private void OnDisable()
    {
        PauseSystem.Instance.Unregister(this);
        CustomInputManager.Instance.PlayerInputActions.Player.Attack.started -= Attack;
    }

    /// <summary>攻撃をする処理</summary>
    /// <param name="context">コールバック</param>
    private void Attack(InputAction.CallbackContext context)
    {
        if(!_isPause)
        {
            if (CurrentAttackInput < _attackBufferedInput)
            {
                CurrentAttackInput++;
                _onCurrentAttackInputChanged?.Invoke(CurrentAttackInput);
            }

            IsAttackAnimation = true;
        }
    }

    //アニメーションイベントで呼んでいる
    private void AttackEnd()
    {
        if (CurrentAttackInput <= 0)
        {
            IsAttackAnimation = false;
            ResetAttackPattern();
        }
        else
        {
            NextAttackPattern();
        }
    }

    //攻撃先行入力の受付を開始する
    private void EnableBufferedInput()
    {
        if (CurrentAttackInput > 0)
        {
            CurrentAttackInput--;
            _onCurrentAttackInputChanged?.Invoke(CurrentAttackInput);
        }
    }

    /// <summary>通常攻撃の当たり判定をとる</summary>
    public void NormalAttack()
    {
        CriAudioManager.Instance.SE.Play("SE", "SE_Player_Attack_01");
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

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }
}