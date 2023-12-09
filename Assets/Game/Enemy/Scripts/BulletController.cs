using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public class BulletController : MonoBehaviour, IPause
{

    [SerializeField]
    private float _speed = 5;

    [SerializeField]
    private float _attackPower = 2;

    private Rigidbody _rb;

    private CancellationTokenSource _cancell = new();

    private Vector3 _velocity;

    private Vector3 _angularVelocity;

    private void Start()
    {
        PauseSystem.Instance.Register(this);
        Interval().Forget();
    }

    public void BulletShot(Vector3 pos)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(pos * _speed, ForceMode.Impulse);
    }

    private async UniTask Interval()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(5f), cancellationToken : _cancell.Token);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamage>(out IDamage damage))
        {
            damage.SendDamage(_attackPower);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        PauseSystem.Instance.Unregister(this);
        _cancell?.Cancel();
    }

    public void Pause()
    {
        _velocity = _rb.velocity;
        _angularVelocity = _rb.angularVelocity;
        _rb.isKinematic = true;
        _cancell?.Cancel();
        _cancell = new();
    }

    public void Resume()
    {
        _rb.isKinematic = false;
        _rb.velocity = _velocity;
        _rb.angularVelocity = _angularVelocity;
        Interval().Forget();
    }
}
