using UnityEngine;

[CreateAssetMenu(menuName = "State/WalkToPoint")]
public class WalkToPointState : State
{
    public override void Init()
    {
        Agent.SetDestination(Controller.Target.position);
    }
    public override void Run()
    {
        if (IsFinished) return;
        Move();
    }
    public void Move()
    {
        var distance = Agent.remainingDistance;
        if (distance > Agent.stoppingDistance)
        {
            Movement.Move();
        }
        else
        {
            Animator.SetBool("Move", false);
            IsFinished = true;
        }
    }
}
