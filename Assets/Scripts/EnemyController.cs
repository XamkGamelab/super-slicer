using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IDamageable
{
    private GameManager gameManager;
    [SerializeField] float speed;
    [SerializeField] GameObject player;
    [SerializeField] int points;
    private Transform playerPos;
    [SerializeField] private float attackCD = 2f;
    private float currentAttackCD = 0f;
    private bool attackOnCD = false;
    PlayerController playerController;

    [SerializeField] Slider healthSlider;
    [SerializeField] Transform body;
    [SerializeField] Transform moveDir;
    [SerializeField] int health = 3;

    [SerializeField] UIManager uiManager;

    public int Health { get; set; }
    public int PointValue { get; set; }

    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        uiManager.IncreaseScore(PointValue);
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = gameManager.UIManager;
        player = gameManager.player;
        playerPos = player.transform;
        playerController = player.GetComponent<PlayerController>();
        Health = health;
        healthSlider.maxValue = health;
        UpdateHealthBar();
        PointValue = points;
    }

    void Update()
    {
        moveDir.rotation = Quaternion.LookRotation(Vector3.forward, playerPos.position - transform.position);
        transform.position += moveDir.up * speed * Time.deltaTime;

        if (attackOnCD) 
        { 
            currentAttackCD -= Time.deltaTime;
            if (currentAttackCD < 0)
            {
                attackOnCD = false;
            }
        }
    }

    public bool Attack()
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

        if (collision.gameObject.CompareTag("Player") && Attack())
        {
            playerController.Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enemyattack " + attackOnCD);

        if (collision.gameObject.CompareTag("Player") && Attack())
        {

            playerController.Damage();
        }
    }

    private void OnEnemyDeath()
    {
        uiManager.IncreaseScore(PointValue);
        Destroy(gameObject);
    }

    public void Damage()
    {
        if (Health >= 0)
        {
            Health -= playerController.AttackDamage();
            UpdateHealthBar();
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = Health;
    }
}
