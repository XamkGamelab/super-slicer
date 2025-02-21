using UnityEngine;
using static GameManager;

public class GameController : MonoBehaviour
{
    private GameManager gameManager;
    GameManager.StateType state;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        switch (state)
        {
            case (GameManager.StateType PAUSED):
                break;
            default:
                break;
        }
    }
}
