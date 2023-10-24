using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField, Tooltip("歩いているときのスピード")] 
    private float _walkSpeed = 5F;

    [SerializeField, Tooltip("走っているときのスピード")]
    private float _runSpeed = 7F;
    
    [SerializeField, Tooltip("一回転かける秒数")] 
    private float _turnTime = 1.0F;
    
    /// <summary>現在の移動方向</summary>
    private Quaternion _currentMoveVector = default;
    
    /// <summary>一個前の移動方向</summary>
    private Quaternion _lastMoveVector = default;

    /// <summary>その回転にかける時間</summary>
    private float _rotateTimer = 0.0F;

    /// <summary>タイマー</summary>
    private float _timer = 0.0F;
    
    private Rigidbody _rb = default;

    /// <summary>入力された方向</summary>
    public Vector2 InputDir { get; private set; }
    
    
    /// <summary>現在のスピード</summary>
    private float _currentSqrtSpeed = 0;

    /// <summary>現在のスピード</summary>
    public float CurrentSqrtSpeed
    {
        get => _currentSqrtSpeed;
        private set
        {
            _onCurrentSqrtSpeedChanged?.Invoke(value);
            _currentSqrtSpeed = value;
        }
    }

    ///<summary>現在のスピードが変更された際に呼ばれるEvent</summary> 
    private Action<float> _onCurrentSqrtSpeedChanged = default;

    /// <summary>現在のスピードが変更された際に呼ばれるEvent</summary>
    public event Action<float> OnCurrentSqrtSpeedChanged
    {
        add => _onCurrentSqrtSpeedChanged += value;
        remove => _onCurrentSqrtSpeedChanged -= value;
    }


    /// <summary>現在走っているかどうか</summary>
    private bool _isRunning = false;

    /// <summary>現在走っているかどうか</summary>
    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            _onIsRunningChanged?.Invoke(value);
            _isRunning = value;
        }
    }

    /// <summary>IsRunningが変更された際に呼ばれるAction</summary>
    private Action<bool> _onIsRunningChanged = default;
    
    /// <summary>IsRunningが変更された際に呼ばれるAction</summary>
    public event Action<bool> OnIsRunningChanged
    {
        add => _onIsRunningChanged += value;
        remove => _onIsRunningChanged -= value;
    }
    
    
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // スティックの入力を取得
        CustomInputManager.Instance.PlayerInputActions.Player.Move.started += DirUpdate;
        CustomInputManager.Instance.PlayerInputActions.Player.Move.performed += DirUpdate;
        CustomInputManager.Instance.PlayerInputActions.Player.Move.canceled += DirUpdate;
        // 走る関係
        CustomInputManager.Instance.PlayerInputActions.Player.Run.started += StartRunning;
        CustomInputManager.Instance.PlayerInputActions.Player.Run.canceled += EndRunning;
    }

    private void OnDisable()
    {
        // スティックの入力を取得
        CustomInputManager.Instance.PlayerInputActions.Player.Move.started -= DirUpdate;
        CustomInputManager.Instance.PlayerInputActions.Player.Move.performed -= DirUpdate;
        CustomInputManager.Instance.PlayerInputActions.Player.Move.canceled -= DirUpdate;
        // 走る関係
        CustomInputManager.Instance.PlayerInputActions.Player.Run.started -= StartRunning;
        CustomInputManager.Instance.PlayerInputActions.Player.Run.canceled -= EndRunning;
    }

    /// <summary>FixedUpdate時に呼び出される</summary>
    public void OnFixedUpdate()
    {
        if (InputDir != Vector2.zero)
        {
            var moveDir = Camera.main.transform.TransformDirection(new Vector3(InputDir.x, 0.0F, InputDir.y));
            moveDir = new Vector3(moveDir.x, 0, moveDir.z).normalized;
            
            // 方向転換
            UpdatePlayerDir(moveDir);

            if (IsRunning)
            {
                Move(_runSpeed, moveDir);
            } // 走っているときの処理
            else
            {
                Move(_walkSpeed, moveDir);
            } // 歩いているときの処理
        }
        
        Rotate();
    }
    
    /// <summary>移動処理</summary>
    /// <param name="speed">スピード</param>
    private void Move(float speed, Vector3 moveDir)
    {
        // 移動
        var playerForward = moveDir * speed;
        _rb.velocity = new Vector3(playerForward.x, _rb.velocity.y, playerForward.z);
    }
    
    /// <summary>回転移動</summary>
    private void Rotate()
    {
        float tempTurnTime = _turnTime;
        
        this.transform.rotation = Quaternion.Slerp(_lastMoveVector, _currentMoveVector, Mathf.Clamp01(_timer / _rotateTimer));

        _timer += Time.fixedDeltaTime;
    }

    /// <summary>方向転換</summary>
    private void UpdatePlayerDir(Vector3 targetDir)
    {
        _lastMoveVector = this.transform.rotation;
        _currentMoveVector = Quaternion.LookRotation(targetDir);

        var temp = Mathf.Abs(_lastMoveVector.eulerAngles.y - _currentMoveVector.eulerAngles.y);
        _rotateTimer = (temp / 360.0F) * _turnTime;

        _timer = 0.0F;
        
        Rotate();
    }

    /// <summary>移動停止</summary>
    public void Stop()
    {
        _rb.velocity = new Vector3(0.0F, _rb.velocity.y, 0.0F);
    }

    /// <summary>インプットされた方向を更新する</summary>
    /// <param name="callback">コールバック</param>
    private void DirUpdate(InputAction.CallbackContext callback)
    {
        InputDir = callback.ReadValue<Vector2>();
        CurrentSqrtSpeed = InputDir.sqrMagnitude * (IsRunning ? _runSpeed : _walkSpeed);
    }

    /// <summary>走り始めた処理</summary>
    /// <param name="callback">コールバック</param>
    private void StartRunning(InputAction.CallbackContext callback)
    {
        IsRunning = true;
        CurrentSqrtSpeed = InputDir.sqrMagnitude * (IsRunning ? _runSpeed : _walkSpeed);
    }

    /// <summary>走り終えた処理</summary>
    /// <param name="callback">コールバック</param>
    private void EndRunning(InputAction.CallbackContext callback)
    {
        IsRunning = false;
        CurrentSqrtSpeed = CurrentSqrtSpeed = InputDir.sqrMagnitude * (IsRunning ? _runSpeed : _walkSpeed);
    }
}
