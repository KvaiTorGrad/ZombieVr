using UnityEngine;

public class Controller : MonoBehaviour, IController
{
    public IMovement Movement { get; set; }
    public IAttack Attack { get; set; }
    public static InputSystem InputActions { get; private set; }
    private void Awake()
    {
        InputActions = new InputSystem();
        InputActions.Enable();
        Movement = GetComponent<IMovement>();
        Attack = GameObject.FindGameObjectWithTag("Weapons").GetComponentInChildren<IAttack>();
    }
}
