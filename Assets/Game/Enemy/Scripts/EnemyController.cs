using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamage
{
    [SerializeField]
    private EnemyStateMachine _stateMachine = new();

    [SerializeField, Tooltip("Playerの座標")]
    private Transform _playerTransform;

    [SerializeField, Tooltip("敵のタイプ")]
    private EnemyType _enemyType = EnemyType.Short;

    [SerializeField]
    private IdleType _idleType = IdleType.Normal;

    [SerializeField, Tooltip("ヒットポイント")]
    private float _hp = 10f;

    [SerializeField, Tooltip("Playerのレイヤー")]
    private LayerMask _targetLayer;

    [SerializeField]
    private EnemyMove _enemyMove = new();

    [SerializeField]
    private EnemyAttack _enemyAttack = new();

    [SerializeField]
    private Animator _anim;

    private Rigidbody _rb;

    private NavMeshAgent _agent;

    public EnemyStateMachine StateMachine => _stateMachine;

    public Transform PlayerTransform => _playerTransform;

    public LayerMask TargetLayer => _targetLayer;

    public EnemyType EnemyType => _enemyType;

    public IdleType IdleType => _idleType;

    public EnemyMove EnemyMove => _enemyMove;

    public EnemyAttack EnemyAttack => _enemyAttack;

    public NavMeshAgent Agent => _agent;

    //public Animator Anim => _anim;
    public Animator Anim { get => _anim; set => _anim = value; }


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine.Set(this);
    }

    private void Start()
    {
        _enemyMove.Init(transform, _playerTransform, _rb, _agent, _anim);
        _enemyAttack.Init(transform, _targetLayer, _anim);

        if (_idleType == IdleType.Normal)
        {
            _stateMachine.StartState(_stateMachine.Idle);
        }
        else
        {
            _stateMachine.StartState(_stateMachine.Patrol);
        }
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

    //private void OnAttack()
    //{
    //    _enemyAttack.AttackAsync();
    //}

    void OnDrawGizmos()
    {
        //近距離用の攻撃範囲ギズモ
        if (_enemyType == EnemyType.Short)
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.TransformPoint(_enemyAttack.ShortAttackCenter),
                transform.rotation, _enemyAttack.ShortAttackSize);
            Gizmos.DrawWireCube(Vector3.zero, _enemyAttack.ShortAttackSize);
        }
        //遠距離用の攻撃範囲ギズモ
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * _enemyAttack.LongAttackRange);
        }
    }
}

public enum EnemyType
{
    Short,
    Long,
}

public enum IdleType
{
    Normal,
    Patrol,
}

