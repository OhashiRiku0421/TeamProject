using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData: ScriptableObject
{

    [SerializeField, Header("攻撃のインターバル")]
    private float _awaitAttack = 3;

    public float AwaitAttack => _awaitAttack;

    [SerializeField, Header("攻撃力")]
    private float _attackPower = 1;

    public float AttackPower => _attackPower;

    [SerializeField, Header("遠距離攻撃の範囲")]
    private float _longAttackRange = 10f;

    public float LongAttackRange => _longAttackRange;

    [SerializeField, Header("近距離攻撃の当たり判定のサイズ")]
    private Vector3 _shortAttackSize;

    public Vector3 ShortAttackSize => _shortAttackSize;

    [SerializeField, Header("近距離攻撃の当たり判定の中心")]
    private Vector3 _shortAttackCenter;

    public Vector3 ShortAttackCenter => _shortAttackCenter;

    [SerializeField, Header("扇型の角度")]
    private float _fanAngle = 90f;

    public float FanAngle => _fanAngle;

    [SerializeField, Header("移動のスピード")]
    private float _moveSpeed = 5;

    public float MoveSpeed => _moveSpeed;

    [SerializeField, Header("巡回スピード")]
    private float _patrolSpeed = 3;

    public float PatrolSpeed => _patrolSpeed;

    [SerializeField, Header("移動範囲")]
    private float _moveDistance = 10;

    public float MoveDistance => _moveDistance;

    [SerializeField, Header("近づける距離")]
    private float _stopDistance;

    public float StopDistance => _stopDistance;
}
