using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;

    private GameManager gameManager;
    int score = 0;

    public enum Menus
    {
        MainMenu,
        PauseMenu,
        GameOverMenu,
        HUD
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {

    }

    public void Play()
    {
        gameManager.Pause(false);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        gameManager.QuitGame();
    }

    public void MainMenu()
    {

        // TODO: use previous timescale instead if paused at TS 0
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        // TODO: Settings
    }
}
