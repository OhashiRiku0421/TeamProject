using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMoveController : MonoBehaviour, IPause
{
    [SerializeField, Tooltip("通常時の移動のスピード")]
    private float _moveSpeed = 5F;

    [SerializeField, Tooltip("空中にいる時の移動のスピード")]
    private float _jumpMoveSpeed = 5F;

    [SerializeField, Tooltip("一回転かける秒数")] private float _turnTime = 1.0F;

    [SerializeField, Tooltip("ジャンプの力")] private float _jumpPower = 5.0f;

    /// <summary>現在の移動方向</summary>
    private Quaternion _currentMoveVector = default;

    /// <summary>一個前の移動方向</summary>
    private Quaternion _lastMoveVector = default;

    /// <summary>その回転にかける時間</summary>
    private float _rotateTimer = 0.0F;

    /// <summary>タイマー</summary>
    private float _timer = 0.0F;

    private Rigidbody _rb = default;

    private bool _isPause = false;

    private Vector3 _velocity;

    private Vector3 _angularVelocity;

    /// <summary>入力された方向</summary>
    public Vector2 InputDir { get; private set; }


    /// <summary>現在のスピード</summary>
    private float _currentSqrtSpeed = 0;

    /// <summary>メインカメラ</summary>
    private Camera _mainCamera = default;

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

    /// <summary>ジャンプ中かどうか</summary>
    private bool _isJumping = false;

    /// <summary>ジャンプ中かどうか</summary>
    public bool IsJumping
    {
        get => _isJumping;
        private set
        {
            if (value != _isJumping)
            {
                _onIsJumpingChanged?.Invoke(value);
                _isJumping = value;
            }
        }
    }

    /// <summary>IsJumpingが変更された際に呼ばれるEvent</summary>
    private Action<bool> _onIsJumpingChanged = default;

    /// <summary>IsJumpingが変更された際に呼ばれるEvent</summary>
    public event Action<bool> OnIsJumpingChanged
    {
        add => _onIsJumpingChanged += value;
        remove => _onIsJumpingChanged -= value;
    }

    /// <summary>現在接地しているかどうか</summary>
    private bool _isGround = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        // スティックの入力を取得
        CustomInputManager.Instance.PlayerInputActions.Player.Move.started += DirUpdate;
        CustomInputManager.Instance.PlayerInputActions.Player.Move.performed += DirUpdate;
        CustomInputManager.Instance.PlayerInputActions.Player.Move.canceled += DirUpdate;

        CustomInputManager.Instance.PlayerInputActions.Player.Jump.started += JumpInput;
        CustomInputManager.Instance.PlayerInputActions.Player.Jump.canceled += CancelJumpInput;

        PauseSystem.Instance.Register(this);
    }

    private void OnDisable()
    {
        // スティックの入力を取得
        CustomInputManager.Instance.PlayerInputActions.Player.Move.started -= DirUpdate;
        CustomInputManager.Instance.PlayerInputActions.Player.Move.performed -= DirUpdate;
        CustomInputManager.Instance.PlayerInputActions.Player.Move.canceled -= DirUpdate;

        CustomInputManager.Instance.PlayerInputActions.Player.Jump.started -= JumpInput;
        CustomInputManager.Instance.PlayerInputActions.Player.Jump.canceled -= CancelJumpInput;

        PauseSystem.Instance.Unregister(this);
    }

    /// <summary>MoveStateのFixedUpdate時に呼び出される</summary>
    public void OnFixedUpdateMoveState()
    {
        if (InputDir != Vector2.zero)
        {
            var moveDir = _mainCamera.transform.TransformDirection(new Vector3(InputDir.x, 0.0F, InputDir.y));
            moveDir = new Vector3(moveDir.x, 0, moveDir.z).normalized;

            // 方向転換
            UpdatePlayerDir(moveDir);

            // 歩いているときの処理
            MoveVelocity(_moveSpeed, moveDir);
        }

        Rotate();
    }

    /// <summary>MoveStateのFixedUpdate時に呼び出される</summary>
    public void OnFixedUpdateJumpState()
    {
        if (InputDir != Vector2.zero)
        {
            var moveDir = _mainCamera.transform.TransformDirection(new Vector3(InputDir.x, 0.0F, InputDir.y));
            moveDir = new Vector3(moveDir.x, 0, moveDir.z).normalized;

            // 方向転換
            UpdatePlayerDir(moveDir);

            // 歩いているときの処理
            MoveAddForce(_jumpMoveSpeed, _moveSpeed, moveDir);
        }

        Rotate();
    }

    private void MoveVelocity(float speed, Vector3 moveDir)
    {
        // 移動
        var playerForward = moveDir * speed;
        _rb.velocity = new Vector3(playerForward.x, _rb.velocity.y, playerForward.z);
    }

    private void MoveAddForce(float speed, float limitSpeed, Vector3 moveDir)
    {
        var playerForward = moveDir * speed;
        _rb.AddForce(playerForward, ForceMode.Force);

        if (_rb.velocity.magnitude > limitSpeed)
        {
            var vec = _rb.velocity.normalized * limitSpeed;
            _rb.velocity = new Vector3(vec.x, _rb.velocity.y, vec.z);
        }
    }

    /// <summary>回転移動</summary>
    private void Rotate()
    {
        float tempTurnTime = _turnTime;

        this.transform.rotation =
            Quaternion.Slerp(_lastMoveVector, _currentMoveVector, Mathf.Clamp01(_timer / _rotateTimer));

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
        if(!_isPause)
        {
            InputDir = callback.ReadValue<Vector2>();
            CurrentSqrtSpeed = InputDir.sqrMagnitude;
        }
    }

    private void PlayMoveSE()
    {
        if (_currentGround == CurrentGround.Mad)
        {
            CriAudioManager.Instance.SE.Play("SE", "SE_Player_Footstep_01");
        }
        else
        {
            CriAudioManager.Instance.SE.Play("SE", "SE_Player_Footstep_02");
        }
    }

    private CurrentGround _currentGround = CurrentGround.Mad;

    private enum CurrentGround
    {
        Mad,
        Stone,
    }

    //--- ジャンプの処理 ---

    /// <summary>ジャンプの入力があった際の処理</summary>
    /// <param name="context">コールバック</param>
    private void JumpInput(InputAction.CallbackContext context)
    {
        if(!_isPause) IsJumping = true;
    }

    /// <summary>ジャンプの入力が解除された際の処理</summary>
    /// <param name="context">コールバック</param>
    private void CancelJumpInput(InputAction.CallbackContext context)
    {
        if (_isGround && !_isPause)
        {
            IsJumping = false;
        }
    }

    /// <summary>ジャンプステートに入った際の処理</summary>
    public void OnJumpStateEntry()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _jumpPower, _rb.velocity.z);
        CriAudioManager.Instance.SE.Play("SE", "SE_Player_Jump_st");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
            IsJumping = false;

            CriAudioManager.Instance.SE.Play("SE", "SE_Player_Jump_ed");
        }
    }

    public void Pause()
    {
        _isPause = true;
        _velocity = _rb.velocity;
        _angularVelocity = _rb.angularVelocity;
        _rb.isKinematic = true;
    }

    public void Resume()
    {
        _isPause = false;
        _rb.isKinematic = false;
        _rb.velocity = _velocity;
        _rb.angularVelocity = _angularVelocity;
    }
}