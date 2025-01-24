using UnityEngine;
using static GameManager;

public class GameController : MonoBehaviour
{
    private GameManager gameManager;
    GameManager.StateType state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
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
