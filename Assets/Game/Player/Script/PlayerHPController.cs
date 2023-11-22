using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPController : MonoBehaviour, IDamage
{
    [SerializeField, Tooltip("ゴッドモード")] private bool _isGodMode = false;

    [SerializeField, Tooltip("ゴッドモードHP")] private float _godModeHP = 10000F;

    /// <summary>現在のHP</summary>
    private float _currentHP = 0F;

    /// <summary>CurrentHPが変更された際に呼ばれる</summary>
    private Action<float> _onCurrentHPChanged = default;

    /// <summary>現在のHP</summary>
    public float CurrentHP
    {
        get => _currentHP;
        private set
        {
            if(_currentHP != value)
            {
                _onCurrentHPChanged?.Invoke(value);
                _currentHP = value;
            }
        }
    }

    /// <summary>CurrentHPが変更された際に呼ばれる</summary>
    public event Action<float> OnCurrentHpChanged
    {
        add => _onCurrentHPChanged += value;
        remove => _onCurrentHPChanged -= value;
    }

    /// <summary>プレイヤーのHPが0になった際の処理</summary>
    private Action _onDeadEvent = default;

    /// <summary>プレイヤーのHPが0になった際の処理</summary>
    public event Action OnDeadEvent
    {
        add => _onDeadEvent += value;
        remove => _onDeadEvent -= value;
    }

    private void Start()
    {
        if (_isGodMode)
        {
            _currentHP = _godModeHP;
        }
        else
        {
            _currentHP = ExternalLifeManager.Life;
        }

        Debug.Log(_currentHP);
    }


    public void SendDamage(float damage)
    {
        CurrentHP -= damage;

        // 死んだ際の処理
        if (_currentHP <= 0)
        {
            _onDeadEvent?.Invoke();
            CriAudioManager.Instance.SE.Play("SE", "SE_Player_Dead");
        }
        else
        {
            CriAudioManager.Instance.SE.Play("SE", "SE_Player_Damage");
        }
    }
}