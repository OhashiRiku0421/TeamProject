using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerAvoidController : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("回避距離")] private float _avoidSpeed = 3.0F;

    [SerializeField]
    private float _avoidTime = 0.25f;

    /// <summary>現在回避中かどうか</summary>
    private bool _isAvoiding = false;

    private bool _isPause = false;

    [SerializeField]
    private Rigidbody _rb = default;

    /// <summary>現在回避中かどうか</summary>
    public bool IsAvoiding
    {
        get => _isAvoiding;
        private set
        {
            if (_isAvoiding != value)
            {
                _onIsAvoidingChanged?.Invoke(value);
                _isAvoiding = value;
            }
        }
    }

    /// <summary>IsAvoidingが変更された際によばれるEvent</summary>
    private Action<bool> _onIsAvoidingChanged = default;
    
    /// <summary>IsAvoidingが変更された際によばれるEvent</summary>
    public event Action<bool> OnIsAvoidingChanged
    {
        add => _onIsAvoidingChanged += value;
        remove => _onIsAvoidingChanged -= value;
    }

    /// <summary>現在Tweeningしているかどうか</summary>
    private bool _isTweening = false;

    private void Start()
    {
        //_rb.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        PauseSystem.Instance.Register(this);
        CustomInputManager.Instance.PlayerInputActions.Player.Avoid.started += InputAvoiding;
        CustomInputManager.Instance.PlayerInputActions.Player.Avoid.canceled += CancelInputAvoiding;
    }

    private void OnDisable()
    {
        PauseSystem.Instance.Unregister(this);
        CustomInputManager.Instance.PlayerInputActions.Player.Avoid.started -= InputAvoiding;
        CustomInputManager.Instance.PlayerInputActions.Player.Avoid.canceled -= CancelInputAvoiding;
    }

    /// <summary>InputActionに登録する関数</summary>
    /// <param name="context">コールバック</param>
    private void InputAvoiding(InputAction.CallbackContext context)
    {
       
        if (!_isPause)
        {
            IsAvoiding = true;
        }
    }

    private void CancelInputAvoiding(InputAction.CallbackContext context)
    {
        if (!_isTweening && !_isPause)
        {
            IsAvoiding = false;
        }
    }

    /// <summary>AvoidStateに入ったら行ってほしい処理</summary>
    public void OnSteteEntry()
    {
        //transform.DOMove(transform.position + (transform.forward * _avoidDistance), _avoidDuration)
        //    .OnStart(() => _isTweening = true)
        //    .OnComplete(() =>
        //    {
        //        _isTweening = false;
        //        IsAvoiding = false;
        //    });

        _rb.AddForce(transform.forward * _avoidSpeed, ForceMode.Impulse);
        StartCoroutine(AvoidAsync());
        CriAudioManager.Instance.SE.Play("SE", "SE_Player_Avoid");
        CriAudioManager.Instance.SE.Play("VOICE", "VC_Player_Avoid");
    }

    IEnumerator AvoidAsync()
    {
        _isTweening = true;
        yield return new WaitForSeconds(_avoidTime);
        _rb.velocity = Vector3.zero;
        _isTweening = false;
        IsAvoiding = false;

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
