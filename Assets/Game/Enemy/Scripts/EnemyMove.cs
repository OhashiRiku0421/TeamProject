using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyMove
{
    [SerializeField]
    private Transform[] _patrolPoints;

    private int _destPoint = 0;

    private NavMeshAgent _agent;

    private Transform _transform;

    private Transform _playerTransform;

    private Animator _anim;

    private EnemyData _data;

    public void Init(Transform transform, Transform playerTransform,
        NavMeshAgent agent, Animator anim, EnemyData data)
    {
        _transform = transform;
        _playerTransform = playerTransform;
        _agent = agent;
        _anim = anim;
        _data = data;
    }

    /// <summary>
    /// 扇型の視界に入っているかどうかを判定
    /// </summary>
    public bool IsMove()
    {
        float distance = Vector3.Distance(_transform.position, _playerTransform.position);

        if (distance <= _data.MoveDistance)
        {
            // 扇型の中心から対象オブジェクトへの方向ベクトルを計算
            Vector3 direction = _playerTransform.position - _transform.position;

            // 扇型の角度と方向ベクトルの角度を比較
            float angle = Vector3.Angle(_transform.forward, direction);

            if (angle <= _data.FanAngle / 2)
            {
                //障害物が無いか判定
                if (Physics.Raycast(_transform.position, direction, out RaycastHit hit))
                {
                    if(hit.collider.transform == _playerTransform) return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Enemyの移動
    /// </summary>
    public void Move()
    {
        //移動
        _agent.destination = _playerTransform.position;
    }

    public void MoveStop()
    {
        _agent.destination = _transform.position;
    }

    /// <summary>
    /// Enemyの向き
    /// </summary>
    public void Rotation()
    {
        //向き
        Vector3 dir = _playerTransform.position - _transform.position;
        dir.y = 0;//y軸は固定
        var rotate = Quaternion.LookRotation(dir);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, rotate, 0.01f);
    }

    public void Patrol()
    {
        if (_patrolPoints.Length == 0) return;

        _agent.destination = _patrolPoints[_destPoint].position;

        _destPoint = (_destPoint + 1) % _patrolPoints.Length;
    }

    public void SetMoveSpeed()
    {
        _agent.speed = _data.MoveSpeed;
    }

    public void SetPatrolSpeed()
    {
        _agent.speed = _data.PatrolSpeed;
    }
}
