using UnityEngine;
using UnityEngine.AI;
public abstract class State : ScriptableObject
{
    public bool IsFinished { get; protected set; }
    public IMovement Movement { get; set; }
    public IHandAttack HandAttack { get; set; }
    public IAIController Controller { get; set; }
    public IHp Hp { get; set; }
    [HideInInspector] public NavMeshAgent Agent { get; set; }
    [HideInInspector] public Animator Animator { get; set; }
    public virtual void Init() { }
    public abstract void Run();
}
