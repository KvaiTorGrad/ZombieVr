using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IController _controller;
    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        _controller = player.GetComponent<IController>();
    }
    void Update()
    {
        if (!GameManager.IsGameOver && GameManager.IsGameStart)
        {
            _controller.Movement.Move();
            _controller.Movement.Rotate();
            _controller.Attack.Attack();
        }
    }
}
