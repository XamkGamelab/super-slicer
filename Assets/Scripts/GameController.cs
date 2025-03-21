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
            case (GameManager.StateType.PAUSED):
                Time.timeScale = 0;
                break;
            case (GameManager.StateType.GAMEOVER):
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            default:
                break;
        }
    }
}
