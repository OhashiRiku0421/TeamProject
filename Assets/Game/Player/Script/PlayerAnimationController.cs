using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField, Tooltip("アニメーター")] private Animator _animator = default;

    [SerializeField, Tooltip("PlayerのMoveコントローラー")]
    private PlayerMoveController _moveController = default;

    [SerializeField, Tooltip("PlayerのAttackコントローラー")]
    private PlayerAttackController _attackController = default;

    [SerializeField, Tooltip("PlayerのAvoidコントローラー")]
    private PlayerAvoidController _avoidController = default;

    [SerializeField, Tooltip("PlayerのCollectコントローラー")]
    private PlayerCollectController _collectController = default;

    [SerializeField, Tooltip("PlayerのHPコントローラー")]
    private PlayerHPController _hpController = default;
    
    /// <summary>アニメーターのSpeedパラメータのハッシュ</summary>
    private readonly int _speedParamHash = Animator.StringToHash("Speed");

    /// <summary>アニメーターのAttackInputCountパラメータのハッシュ</summary>
    private readonly int _attackInputParamHash = Animator.StringToHash("AttackInputCount");

    /// <summary>アニメーターのAttackPatternパラメータのハッシュ</summary>
    private readonly int _attackPatternParamHash = Animator.StringToHash("AttackPattern");

    /// <summary>アニメーターのIsCollectingパラメータのハッシュ</summary>
    private readonly int _isCollectingParamHash = Animator.StringToHash("IsCollecting");

    /// <summary>アニメーターのIsAvoidingパラメータのハッシュ</summary>
    private readonly int _isAvoidingParamHash = Animator.StringToHash("IsAvoiding");

    /// <summary>アニメーターのOnHitDamageパラメータのハッシュ</summary>
    private readonly int _onHitDamageParamHash = Animator.StringToHash("OnHitDamage");
    
    /// <summary>アニメーターのOnHitDamageパラメータのハッシュ</summary>
    private readonly int _isJumpingParamHash = Animator.StringToHash("IsJumping");
    private void OnEnable()
    {
        _moveController.OnCurrentSqrtSpeedChanged += AnimParamSpeedUpdate;
        _moveController.OnIsJumpingChanged += AnimParamOnJumpingUpdate;

        _attackController.OnCurrentAttackInputChanged += AnimAttackInputUpdate;
        _attackController.OnCurrentAttackPatternChanged += AnimAttackPatternUpdate;

        _collectController.OnIsCollectingChanged += AnimParamIsCollectingUpdate;

        _avoidController.OnIsAvoidingChanged += AnimParamIsAvoidingUpdate;

        _hpController.OnCurrentHpChanged += AnimParamOnHitDamageUpdate;
    }

    private void OnDisable()
    {
        _moveController.OnCurrentSqrtSpeedChanged -= AnimParamSpeedUpdate;
        _moveController.OnIsJumpingChanged -= AnimParamOnJumpingUpdate;
        
        _attackController.OnCurrentAttackInputChanged -= AnimAttackInputUpdate;
        _attackController.OnCurrentAttackPatternChanged -= AnimAttackPatternUpdate;
        
        _collectController.OnIsCollectingChanged -= AnimParamIsCollectingUpdate;
        
        _avoidController.OnIsAvoidingChanged -= AnimParamIsAvoidingUpdate;
        
        _hpController.OnCurrentHpChanged -= AnimParamOnHitDamageUpdate;
    }

    /// <summary>AttackInputCountを呼ぶ関数</summary>
    /// <param name="value">value</param>
    private void AnimAttackInputUpdate(int value) => _animator.SetInteger(_attackInputParamHash, value);

    /// <summary>AttackPatternを変更する関数</summary>
    /// <param name="value">value</param>
    private void AnimAttackPatternUpdate(int value) => _animator.SetInteger(_attackPatternParamHash, value);

    /// <summary>AnimatorのSpeedを変更する関数</summary>
    /// <param name="speed">現在のSpeed</param>
    private void AnimParamSpeedUpdate(float speed) => _animator.SetFloat(_speedParamHash, speed);

    /// <summary>AnimatorのIsCollectingを更新する関数</summary>
    /// <param name="value">現在のIsCollecting</param>
    private void AnimParamIsCollectingUpdate(bool value) => _animator.SetBool(_isCollectingParamHash, value);

    /// <summary>AnimatorのIsAvoidingを更新する関数</summary>
    /// <param name="value">現在のIsAvoiding</param>
    private void AnimParamIsAvoidingUpdate(bool value) => _animator.SetBool(_isAvoidingParamHash, value);

    /// <summary>AnimatorのOnHitDamageを更新する関数</summary>
    /// <param name="value"></param>
    private void AnimParamOnHitDamageUpdate(float value) => _animator.SetTrigger(_onHitDamageParamHash);

    /// <summary>AnimatorのIsJumpingを更新する関数</summary>
    /// <param name="value"></param>
    private void AnimParamOnJumpingUpdate(bool value) => _animator.SetBool(_isJumpingParamHash, value);
}