using RootMotion.Dynamics;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class StateEnemy : CharacterParametrs
{
    private int _id;
    private bool _isDead;

    public int IdEnemy { get => _id; set => _id = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }

    [SerializeField] private EnemyDescriptions _enemyDescriptions;

    private FactoryInit _factory;
    private EnemyModel enemyModel;
    private PuppetMaster _puppetMaster;
    private NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMesh { get => _navMeshAgent; set => _navMeshAgent = value; }
    public PuppetMaster PuppetMaster { get => _puppetMaster; set => _puppetMaster = value; }

    private float _distance;
    public float Distance { get => _distance; set => _distance = value; }

    public delegate void IsDeathPlayer(float damage);
    public static event IsDeathPlayer DeadPlayer;

    protected override void Awake()
    {
        base.Awake();
        NavMesh = GetComponent<NavMeshAgent>();
        Source = GetComponentInChildren<AudioSource>();
        PuppetMaster = GetComponentInChildren<PuppetMaster>();
        _factory = new FactoryInit();
        _factory.Init(_enemyDescriptions);
        enemyModel = _factory.CreateMobModel(0, Random.Range(0, 1));
    }

    protected override void Start()
    {
        ResetGame();
    }
    public override void ResetGame()
    {
        Speed = enemyModel.SetSpeed;
        NavMesh.speed = enemyModel.SetSpeed;
        Hp = enemyModel.SetHp;
        Damage = enemyModel.SetForce;
        IsDead = false;
        PuppetMaster.state = RootMotion.Dynamics.PuppetMaster.State.Alive;
    }

    public void DealDamage()
    {
        DeadPlayer(Damage);
    }

}
