using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private GameManager gameManager;
    [SerializeField] float speed;
    [SerializeField] GameObject player;
    private Transform playerPos;
    [SerializeField] private float attackCD = 2f;
    private float currentAttackCD = 0f;
    private bool attackOnCD = false;
    PlayerController playerController;
    //LayerMask mask;

    int health = 3;

    public int Health { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        player = gameManager.player;
        playerPos = player.transform;
        //mask = LayerMask.GetMask("Player");
        playerController = player.GetComponent<PlayerController>();
        Health = health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, playerPos.position - transform.position);
        transform.position += transform.up * speed * Time.deltaTime;

        if (attackOnCD) 
        { 
            currentAttackCD -= Time.deltaTime;
            if (currentAttackCD < 0)
            {
                attackOnCD = false;
            }
        }
    }

    void Attack()
    {
        if (!attackOnCD)
        {
            currentAttackCD = attackCD;
            attackOnCD = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Attack();
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.Damage();
        }
    }

    public void Damage()
    {
        Debug.Log("Got Hit!");
        if (Health >= 0)
        {
            Health--;
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
