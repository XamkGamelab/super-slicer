using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class GameController : MonoBehaviour
{
    private GameManager gameManager;
    public GameManager.StateType state;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        switch (state)
        {
            case (GameManager.StateType.MENU):
                Time.timeScale = 0.0f;
                break;
            case (GameManager.StateType.PAUSED):
                gameManager.Pause(true);
                break;
            case (GameManager.StateType.GAMESTART):
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            default:
                break;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
