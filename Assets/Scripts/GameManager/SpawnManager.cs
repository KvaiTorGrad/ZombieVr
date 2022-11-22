using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private void Awake()
    {
        ButtonController.IsStartGameWithJoystick += IsJoystickGame;
    }
    private void IsJoystickGame(bool isStartGameJoystick)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var enemyCreator = GameObject.Find("SystemEnemy").GetComponent<SystemEnemy>();
        enemyCreator.enabled = true;
        if (isStartGameJoystick)
            player.AddComponent<JoystickVrPlayer>();
        else
            player.AddComponent<NoJoystickVrPlayer>();
    }
    private void OnDisable()
    {
        ButtonController.IsStartGameWithJoystick -= IsJoystickGame;
    }
}
