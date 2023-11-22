using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class BulletController : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5;

    [SerializeField]
    private float _attackPower = 2;

    private Rigidbody _rb;

    private void Start()
    {
        Interval().Forget();
    }

    public void BulletShot(Vector3 pos)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(pos * _speed, ForceMode.Impulse);
    }

    private async UniTask Interval()
    {
        var token = this.GetCancellationTokenOnDestroy();
        await UniTask.Delay(TimeSpan.FromSeconds(5f), cancellationToken : token);
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
}
