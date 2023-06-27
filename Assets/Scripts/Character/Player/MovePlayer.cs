using UnityEngine;

public class MovePlayer : Movement, IMovement
{
    private uint _speed;
    public uint Speed { get; set; }
    private CharacterController _characterController;

    protected override void Awake() 
    {
        _characterController = GetComponent<CharacterController>();  
    }
    protected override void Start()
    {
        _speed = 2;
    }
    public void Move()
    {
        Vector3 input = new Vector3(Controller.InputActions.Player.Move.ReadValue<Vector2>().x,0, Controller.InputActions.Player.Move.ReadValue<Vector2>().y);
        input = Camera.main.transform.TransformDirection(input);
        input.y = -1;
        _characterController.Move(_speed * Time.deltaTime * input);
    }
    public void Rotate()
    {
        transform.rotation = new Quaternion(0, Camera.main.transform.rotation.y, 0,1);
    }
}
