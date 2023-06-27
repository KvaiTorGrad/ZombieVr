using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour, IAIController
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private Transform _target;
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Animator Animator { get => _animator; set => _animator = value; }
    public Transform Target { get => _target; set => _target = value; }
    [SerializeField] private State _attackState;
    [SerializeField] private State _walkToPointState;
    [SerializeField] private State _dead;
    [SerializeField] private State _currentState;

    private IMovement _movement;
    private IHp _hp;
    private IHandAttack _handAttack;
    public State AttackState { get => _attackState; }
    public State WalkToPointState { get => _walkToPointState; }
    public State Dead { get => _dead; }
    public State CurrentState { get => _currentState; set => _currentState = value; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _movement = GetComponent<IMovement>();
        _handAttack = GetComponentInChildren<IHandAttack>();
        _hp = GetComponent<IHp>();
    }
    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        SetState(WalkToPointState);
    }
    private void Update()
    {
        if (_hp.Hp == 0 || _hp.Hp > 4294960000)
            SetState(Dead);
        if (!CurrentState.IsFinished)
        {
            CurrentState.Run();
        }
        else
        {
            if (Agent.remainingDistance <= Agent.stoppingDistance)
                SetState(AttackState);
            else if (Agent.remainingDistance > Agent.stoppingDistance)
                SetState(WalkToPointState);
        }
    }

public void SetState(State state)
{
    CurrentState = Instantiate(state);
    CurrentState.Movement = _movement;
    CurrentState.Controller = this;
    CurrentState.Hp = _hp;
    CurrentState.HandAttack = _handAttack;
    CurrentState.Agent = _agent;
    CurrentState.Animator = _animator;
    CurrentState.Init();
}

}
