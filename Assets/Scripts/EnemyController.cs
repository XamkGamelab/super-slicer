using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] float speed;
    [SerializeField] GameObject player;
    private Transform playerPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        player = gameManager.player;
        playerPos = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, playerPos.position - transform.position);
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
