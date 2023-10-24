using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField, Tooltip("アニメーター")]
    private Animator _animator = default;
    [SerializeField, Tooltip("PlayerのMoveコントローラー")]
    private PlayerMoveController _moveController = default;

    [SerializeField, Tooltip("PlayerのAttackコントローラー")]
    private PlayerAttackController _attackController = default;

    /// <summary>アニメーターのSpeedパラメータのハッシュ</summary>
    private readonly int _speedParamHash = Animator.StringToHash("Speed");
    /// <summary>アニメーターのIsRunningパラメータのハッシュ</summary>
    private readonly int _runningParamHash = Animator.StringToHash("IsRunning");
    /// <summary>アニメーターのAttackパラメータのハッシュ</summary>
    private readonly int _attackParamHash = Animator.StringToHash("Attack");
    /// <summary>アニメーターのIsCloseRangeパラメータのハッシュ</summary>
    private readonly int _isCloseRangeParamHash = Animator.StringToHash("IsCloseRange");

    private void OnEnable()
    {
        _moveController.OnCurrentSqrtSpeedChanged += AnimParamSpeedUpdate;
        _moveController.OnIsRunningChanged += AnimParamIsRunningUpdate;

        _attackController.OnIsCloseRangeChanged += AnimIsCloseRangeUpdate;
        _attackController.OnIsAttackAnimationChanged += AnimAttackTriggerUpdate;
    }

    private void OnDisable()
    {
        _moveController.OnCurrentSqrtSpeedChanged -= AnimParamSpeedUpdate;
        _moveController.OnIsRunningChanged -= AnimParamIsRunningUpdate;
        
        _attackController.OnIsCloseRangeChanged -= AnimIsCloseRangeUpdate;
        _attackController.OnIsAttackAnimationChanged -= AnimAttackTriggerUpdate;
    }

    /// <summary>AttackTriggerを呼ぶ関数</summary>
    /// <param name="value">value</param>
    private void AnimAttackTriggerUpdate(bool value)
    {
        if (value)
        {
            _animator.SetTrigger(_attackParamHash);
        }
    }

    /// <summary>AnimatorのIsCloseRangeを変更する</summary>
    /// <param name="value">現在のIsCloseRange</param>
    private void AnimIsCloseRangeUpdate(bool value) => _animator.SetBool(_isCloseRangeParamHash, value);
    
    /// <summary>AnimatorのSpeedを変更する関数</summary>
    /// <param name="speed">現在のSpeed</param>
    private void AnimParamSpeedUpdate(float speed) => _animator.SetFloat(_speedParamHash, speed);

    /// <summary>AnimatorのIsRunningを変更する関数</summary>
    /// <param name="isRunning">現在走っているかどうか</param>
    private void AnimParamIsRunningUpdate(bool isRunning) => _animator.SetBool(_runningParamHash, isRunning);
}
