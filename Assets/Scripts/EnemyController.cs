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
    //LayerMask mask;

    [SerializeField] Slider healthSlider;
    [SerializeField] Transform body;
    [SerializeField] int health = 3;

    [SerializeField] UIManager uiManager;

    //UnityEvent EnemyDeath;

    public int Health { get; set; }
    public int PointValue { get; set; }

    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        uiManager.IncreaseScore(PointValue);
        //EnemyDeath.RemoveAllListeners();
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = gameManager.UIManager;
        player = gameManager.player;
        playerPos = player.transform;
        //mask = LayerMask.GetMask("Player");
        playerController = player.GetComponent<PlayerController>();
        Health = health;
        healthSlider.maxValue = health;
        UpdateHealthBar();
        PointValue = points;

        //if (EnemyDeath != null)
        //{
        //    EnemyDeath = new UnityEvent();
        //}
        //EnemyDeath.AddListener(OnEnemyDeath);
    }

    // Update is called once per frame
    void Update()
    {
        body.rotation = Quaternion.LookRotation(Vector3.forward, playerPos.position - transform.position);
        transform.position += body.up * speed * Time.deltaTime;

        if (attackOnCD) 
        { 
            currentAttackCD -= Time.deltaTime;
            if (currentAttackCD < 0)
            {
                attackOnCD = false;
            }
        }
        //healthSlider.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.y * -1.0f);
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
        //Attack();

        if (collision.gameObject.CompareTag("Player") && Attack())
        {
            playerController.Damage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enemyattack " + attackOnCD);
        //Attack();

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
                //OnEnemyDeath();
            }
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = Health;
    }
}
