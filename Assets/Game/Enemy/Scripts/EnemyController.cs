using UnityEngine;

public class EnemyController : MonoBehaviour, IDamage
{
    [SerializeField]
    private EnemyStateMachine _stateMachine = new();

    [SerializeField, Tooltip("Playerの座標")]
    private Transform _playerTransform = null;

    [SerializeField, Tooltip("移動範囲")]
    private float _moveDistance = 10;

    [SerializeField, Tooltip("近づける距離")]
    private float _stopDistance;

    [SerializeField, Tooltip("近距離攻撃の当たり判定のサイズ")]
    private Vector3 _attackSize;

    [SerializeField, Tooltip("近距離攻撃の当たり判定の中心")]
    private Vector3 _attackCenter;

    [SerializeField, Tooltip("敵のタイプ")]
    private EnemyType _enemyType = EnemyType.Short;

    [SerializeField, Tooltip("ヒットポイント")]
    private float _hp = 10f;

    [SerializeField, Tooltip("遠距離攻撃の範囲")]
    private float _attackRange = 10f;

    [SerializeField]
    private Vector3 _longAttackCenter;

    private Rigidbody _rb;

    public EnemyStateMachine StateMachine => _stateMachine;

    public Transform PlayerTransform => _playerTransform;

    public float MoveDistance => _moveDistance;

    public float StopDistance => _stopDistance;

    public Vector3 AttackSize => _attackSize;

    public Vector3 AttackCenter => _attackCenter;

    public EnemyType EnemyType => _enemyType;

    public float AttackRange => _attackRange;

    public Vector3 LongAttackCenter => _longAttackCenter;

    public Rigidbody Rb { get => _rb; set => _rb = value; }

    private void Awake()
    {
        _stateMachine.Set(this);
    }

    private void Start()
    {
        //最初のステート
        _stateMachine.StartState(_stateMachine.Idle);
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void SendDamage(float damage)
    {
        //ダメージのステートに変更
        _stateMachine.ChangeState(_stateMachine.Damage);
        _hp -= damage;
        if(_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        //近距離用の攻撃範囲ギズモ
        if (_enemyType == EnemyType.Short)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.TransformPoint(_attackCenter), transform.rotation, _attackSize);
            Gizmos.DrawWireCube(Vector3.zero, _attackSize);
        }
        //遠距離用の攻撃範囲ギズモ
        else
        {
            Gizmos.DrawRay(transform.position + _longAttackCenter, transform.forward * _attackRange);
        }
    }
}

public enum EnemyType
{
    Short,
    Long
}

