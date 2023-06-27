using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    public static Action<string> InitButton;
    private static bool _isGameStart;
    private static bool _isGameOver;
    public static bool IsGameStart { get => _isGameStart; private set => _isGameStart = value; }
    public static bool IsGameOver { get => _isGameOver; set => _isGameOver = value; }

    public static Action GameOver;
    private void Awake()
    {
        Time.timeScale = 1f;
        GameOver += ActiveRestartPanel;
        InitButton += InitSelectButton;
        panels[1].SetActive(false);
        _isGameStart = false;
        _isGameOver = false;
    }
    private void InitSelectButton(string init)
    {
        switch (init)
        {
            case "StartButton":
                panels[0].SetActive(false);
                panels[2].SetActive(false);
                _isGameStart = true;
                break;
            case "RestartButton":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }
    private void ActiveRestartPanel()
    {
        _isGameOver = true;
        panels[1].SetActive(true);
        panels[2].SetActive(true);
        Time.timeScale = 0f;
    }
    private void OnDestroy()
    {
        GameOver -= ActiveRestartPanel;
        InitButton -= InitSelectButton;
    }
}
