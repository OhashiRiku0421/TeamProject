using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInputManager
{
    /// <summary>インスタンス</summary>
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
        _playerInputActions = new PlayerInputAction();
        _playerInputActions.Enable();
    }

    /// <summary>PlayerのInput周り</summary>
    private PlayerInputAction _playerInputActions;

    /// <summary>PlayerのInput周り</summary>
    public PlayerInputAction PlayerInputActions => _playerInputActions;
}