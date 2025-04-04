using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public UIManager UIManager;
    //public MenuManager MenuManager;
    public GameController controller;

    void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            //DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
    }

    public enum StateType
    {
        DEFAULT,      //Fall-back state, should never happen
        PAUSED,
        GAMEOVER,
        GAMESTART,
        MENU,         //Player is viewing in-game menu
        OPTIONS       //player is adjusting game options
    };

    private GameManager()
    {
        // initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // because the game manager will be created before the objects
    }

    // Add your game mananger members here
    public void Pause(bool paused)
    {
        Time.timeScale = paused ? 0.0f : 1.0f;
        controller.state = paused ? StateType.PAUSED : StateType.DEFAULT;
    }

    public void GameOver()
    {
        controller.state = StateType.GAMEOVER;
        UIManager.EnableMenu(UIManager.Menus.GameOverMenu);
    }

    public void QuitGame()
    {
        controller.QuitGame();
    }

    public void IncreaseScore(int amount)
    {
        UIManager.IncreaseScore(amount);
    }
}
