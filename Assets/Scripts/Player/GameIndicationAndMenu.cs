using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameIndicationAndMenu : MonoBehaviour
{
   [SerializeField] private Image _hpImage;
    [SerializeField] private GameObject[] _panel;
    [SerializeField] private TMP_Text _killedText;

    public static GameIndicationAndMenu _gameIndicationAndMenu;

    private void Awake()
    {
        ButtonController.IsStartGameWithJoystick += GameStart;
    }
    private void Start()
    {
        _panel[0].SetActive(true);
        _panel[1].SetActive(false);
    }
    private int _killed;
    private void ShowRemainsHp(float hp,float maxHp)
    {
        _hpImage.fillAmount = hp / maxHp;
    }

    private void ShowKillingEnemy(int killed)
    {
        _killed += killed;
        _killedText.text =$"Killed: {_killed}" ;
    }
    private void GameOver()
    {
        Time.timeScale = 0;
        _panel[1].SetActive(true);
    }
    private void GameStart(bool ToGame)
    {
        Time.timeScale = 1;
        _hpImage.fillAmount = 1;
        _killedText.text = "Killed: 0";
        _panel[0].SetActive(false);
        JoystickVrPlayer.MainHpInit += ShowRemainsHp;
        JoystickVrPlayer.CountDeadEnemy += ShowKillingEnemy;
        JoystickVrPlayer.IsGameOver += GameOver;
    }
    private void OnDisable()
    {
        JoystickVrPlayer.CountDeadEnemy -= ShowKillingEnemy;
        JoystickVrPlayer.MainHpInit -= ShowRemainsHp;
        JoystickVrPlayer.IsGameOver -= GameOver;
    } 
    private void OnDestroy()
    {
        JoystickVrPlayer.CountDeadEnemy -= ShowKillingEnemy;
        JoystickVrPlayer.MainHpInit -= ShowRemainsHp;
        JoystickVrPlayer.IsGameOver -= GameOver;
    }
}
