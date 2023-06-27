public interface IController
{ 
    public IMovement Movement { get;set; }
    public IAttack Attack { get;set; }
    public static InputSystem InputActions { get; private set; }
}

