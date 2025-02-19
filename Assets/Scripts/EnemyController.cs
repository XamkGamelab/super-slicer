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

    [SerializeField] int health = 3;

    public int Health { get; set; }

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

    private bool Attack()
    {
        if (!attackOnCD)
        {
            currentAttackCD = attackCD;
            attackOnCD = true;
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("enemyattack " + attackOnCD);
        //Attack();
        
        if (collision.gameObject.CompareTag("Player") && Attack())
        {
            
            playerController.Damage();
        }
    }

    public void Damage()
    {
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
