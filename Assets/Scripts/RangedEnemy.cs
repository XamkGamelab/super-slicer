using UnityEngine;
using UnityEngine.UI;

public class RangedEnemy : MonoBehaviour, IDamageable
{
    private GameManager gameManager;
    [SerializeField] float speed;
    [SerializeField] GameObject player;
    [SerializeField] int points;
    private Transform playerPos;
    [SerializeField] private float attackCD = 2f;
    private float currentAttackCD = 0f;
    public bool attackOnCD = false;
    PlayerController playerController;

    [SerializeField] Slider healthSlider;
    [SerializeField] Transform body;
    [SerializeField] Transform moveDir;
    [SerializeField] int health = 3;
    [SerializeField] float range;
    public bool inRange = false;

    [SerializeField] Shuriken shurikenPrefab;

    public int Health { get; set; }
    public int PointValue { get; set; }

    private void OnDestroy()
    {
        gameManager.IncreaseScore(PointValue);
    }

    void Start()
    {
        gameManager = GameManager.Instance;
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
        if (Vector3.Distance(transform.position, playerPos.position) < range) inRange = true;
        else inRange = false;

        moveDir.rotation = Quaternion.LookRotation(Vector3.forward, playerPos.position - transform.position);

        if (!inRange)
        {
            transform.position += moveDir.up * speed * Time.deltaTime;
        } else Attack();

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
        if (!attackOnCD && inRange)
        {
            Shuriken temp = Instantiate(shurikenPrefab, transform.position, Quaternion.identity);
            temp.direction = playerPos.position - transform.position;
            temp.direction.Normalize();
            currentAttackCD = attackCD;
            attackOnCD = true;
            return true;
        }
        return false;
    }

    private void OnEnemyDeath()
    {
        //uiManager.IncreaseScore(PointValue);
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
