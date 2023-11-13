using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowController : MonoBehaviour
{
    [SerializeField, Tooltip("スピード")] private float _speed = 1.0F;

    /// <summary>与えるダメージ</summary>
    public float Damage { private get; set; }

    private void Start()
    {
        Destroy(this.gameObject, 30F);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 攻撃処理
        if (other.gameObject.TryGetComponent(out IDamage enemyIDamage))
        {
            enemyIDamage.SendDamage(Damage);
        }
    }
}