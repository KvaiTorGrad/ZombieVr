public class MovementEnemy : Movement, IMovement
{
    public uint Speed { get; set; }
    private IAIController controller;

    protected override void Awake()
    {
        controller = GetComponent<IAIController>();
    }
    protected override void Start()
    {
        Speed = 1;
    }

    public void Move()
    {
        controller.Animator.SetBool("Move", true);
        controller.Agent.speed = Speed;
        controller.Agent.SetDestination(controller.Target.position);
    }
}
