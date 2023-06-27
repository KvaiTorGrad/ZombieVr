using UnityEngine;
[CreateAssetMenu(menuName = "State/AttackState")]
public class AttackState : State
{
    public override void Init()
    {
        Agent.SetDestination(Controller.Target.position);
    }
    public override void Run()
    {
        if (IsFinished) return;
        Attack();
    }
    public void Attack()
    {
        Init();
        var distance = Agent.remainingDistance;
        if (distance <= Agent.stoppingDistance)
        {
            HandAttack.Attack();
        }
        else
        {
            IsFinished = true;
        }
    }
}
