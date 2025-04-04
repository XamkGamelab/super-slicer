using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameManager gameManager;

    [Header("EnemyPrefabs")]
    [SerializeField] GameObject meleeEnemy;
    [SerializeField] GameObject RangedEnemy;

    [SerializeField] BoxCollider2D spawnArea;
    [SerializeField] float spawnDelay;
    [SerializeField] float timeFromLastSpawn = 0.0f;
    [SerializeField] int weights;

    [SerializeField] Transform[] spawnPoints;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (timeFromLastSpawn < spawnDelay) 
        { 
            timeFromLastSpawn += Time.deltaTime;
        } else
        {
            timeFromLastSpawn = 0.0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int zoneIndex = Random.Range(0, 4);
        Transform spawnZone = spawnPoints[zoneIndex];
        Debug.Log($"Index: {zoneIndex}");
        Instantiate(RandomEnemy(), RandomPointInBox(spawnZone.position, spawnZone.localScale), Quaternion.identity);
    }

    private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {

        return center + new Vector3(
           (Random.value - 0.5f) * size.x,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }

    private GameObject RandomEnemy()
    {
        int randomNumber = Random.Range(0, 10);
        if (randomNumber < weights)
        {
            return meleeEnemy;
        }
        else return RangedEnemy;
    }
}
