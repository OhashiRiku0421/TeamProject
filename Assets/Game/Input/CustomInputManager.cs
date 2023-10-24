using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputManager
{
    private static CustomInputManager _instance = null;

    public static CustomInputManager Instance
    {
        get
        {
            _instance ??= new CustomInputManager();
            return _instance;
        }
    }

    private CustomInputManager()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    /// <summary>プレイヤーのInputAction</summary>
    private PlayerInputActions _playerInputActions;

    /// <summary>PlayerのInputAction</summary>
    public PlayerInputActions PlayerInputActions => _playerInputActions;
}
