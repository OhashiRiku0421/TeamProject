using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollectController : MonoBehaviour
{
    /// <summary>現在採取中かどうか</summary>
    private bool _isCollecting = false;

    /// <summary>現在採取中かどうか</summary>
    public bool IsCollecting
    {
        get => _isCollecting;
        private set
        {
            if (_isCollecting != value)
            {
                _onIsCollectingChanged?.Invoke(value);
                _isCollecting = value;
            }
        }
    }

    /// <summary>IsCollectingが変更した際に呼ばれる</summary>
    private Action<bool> _onIsCollectingChanged = default;
    
    /// <summary>IsCollectingが変更した際に呼ばれる</summary>
    public event Action<bool> OnIsCollectingChanged
    {
        add => _onIsCollectingChanged += value;
        remove => _onIsCollectingChanged -= value;
    }

    /// <summary>現在採取の入力がされているかどうか</summary>
    private bool _isCollectInputting = false;

    /// <summary>現在採取可能かどうか</summary>
    private bool _isCollectable = false;

    private void OnEnable()
    {
        CustomInputManager.Instance.PlayerInputActions.Player.Collect.started += StartCollect;
        CustomInputManager.Instance.PlayerInputActions.Player.Collect.canceled += CancelCollect;
    }

    private void OnDisable()
    {
        CustomInputManager.Instance.PlayerInputActions.Player.Collect.started -= StartCollect;
        CustomInputManager.Instance.PlayerInputActions.Player.Collect.canceled -= CancelCollect;
    }

    /// <summary>採集の入力が始まった際に呼ばれる</summary>
    /// <param name="context">コールバック</param>
    private void StartCollect(InputAction.CallbackContext context)
    {
        _isCollectInputting = true;
        UpdateIsCollecting();
    }

    /// <summary>採取の入力が終わった際に呼ばれる</summary>
    /// <param name="context">コールバック</param>
    private void CancelCollect(InputAction.CallbackContext context)
    {
        _isCollectInputting = false;
        UpdateIsCollecting();
    }

    /// <summary>IsCollectingか判定する</summary>
    private void UpdateIsCollecting() => IsCollecting = _isCollectable && _isCollectInputting;

    private void OnTriggerEnter(Collider other)
    {
        // TODO: 条件を採取可能なオブジェクトの作り方によって変える
        if (other.gameObject.CompareTag("Collectable"))
        {
            _isCollectable = true;
            UpdateIsCollecting();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            _isCollectable = false;
            UpdateIsCollecting();
        }
    }
}
