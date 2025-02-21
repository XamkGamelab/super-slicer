using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public class PlayerController : MonoBehaviour, IDamageable
{
    private GameManager gameManager;

    [Header("Events")]
    public UnityEvent<Vector2> movementEvent;
    public UnityEvent dashEvent;

    [Header("Variables")]
    [SerializeField] float speed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDistance;
    [SerializeField] float dashTime;
    [SerializeField] float dashCD;
    [SerializeField] float detectionRange;
    [SerializeField] float attackCD;
    private float currentAttackCD = 0.0f;
    private float currentDashCD = 0.0f;
    private float currentDashDistance = 0.0f;
    public bool dashing;
    [SerializeField] Rigidbody2D rb;
    LayerMask mask;

    [SerializeField] UIManager uiManager;
    [SerializeField] MeleeAttack meleeAttack;
    

    private RaycastHit2D[] enemies;

    bool attacking = false;
    [SerializeField] int health = 3;

    public int Health { get; set; }
    bool isDead = false;
    public bool IsAttacking { get; set; }

    public UIManager UIManager
    {
        get => uiManager;
        set => uiManager = value;
    }


    void Start()
    {
        gameManager = GameManager.Instance;
        if (movementEvent != null)
        {
            movementEvent = new UnityEvent<Vector2>();
        }
        movementEvent.AddListener(Move);
        dashEvent.AddListener(Dash);
        //rb = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Level", "Enemy");
        Health = health;
        IsAttacking = attacking;
        currentDashCD = dashCD;

        UIManager = uiManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDashCD > 0.0f) 
        {
            currentDashCD -= Time.deltaTime;
            uiManager.DashSlider.value = dashCD - currentDashCD;
        }

        if (currentAttackCD <= 0.0f)
        {
            NearestEnemy();
        }

        if (currentAttackCD > 0.0f)
        {
            currentAttackCD -= Time.deltaTime;
        } else if (IsAttacking && !dashing && !isDead)
        {
            Attack();
            currentAttackCD = attackCD;
        }
    }

    void Move(Vector2 moveVector)
    {
        if (isDead) { return; }
        Vector2 dir = new Vector2(transform.parent.position.x, transform.parent.position.y) + moveVector.normalized;
        //Debug.Log($"Moving: {moveVector}");
        transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        transform.parent.position = Vector2.Lerp(transform.parent.position, dir, speed * Time.deltaTime);
    }

    void Dash()
    {
        if (isDead) { return; }
        Debug.Log("Dash");

        //transform.position = Vector2.Lerp()//+= transform.up * 2;
        if (!dashing && currentDashCD <= 0.0f)
        {
            StartCoroutine(Dashing());
        }
        dashDistance = 10f;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, dashDistance, mask);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if (hit && dashing)
            {

                if (hit.collider.gameObject.layer == 6)
                {
                    Destroy(hit.collider.transform.parent.gameObject);
                    uiManager.Combo.IncreaseCombo();
                }

                else if (hit.collider.gameObject.layer == 3)
                {
                    dashDistance = hit.distance;
                }
            }
        }
    }

    IEnumerator Dashing()
    {
        dashing = true;
        //rb.simulated = false;
        currentDashDistance = 0.0f;
        currentDashCD = dashCD;
        
        while (currentDashDistance < dashDistance)
        {
            currentDashDistance += dashSpeed * Time.deltaTime;
            transform.parent.position += transform.up * dashSpeed * Time.deltaTime;

            // add check if colliding with edges or enemies

            yield return null;
        }
        dashing = false;
        //rb.simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        /*if (dashing)
        {
            if (collision.gameObject.CompareTag("Entity"))
            {
                Destroy(collision.gameObject);
            }
        }*/
    }

    public void Damage()
    {
        Debug.Log("Got Hit!");
        if (Health >= 0) 
        {
            Health--; 
            uiManager.HealthSlider.value = Health;
            if (Health <= 0)
            {
                isDead = true;
                Invoke("GameOver", 2f);
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        isDead = true;
        gameManager.GameOver();
    }

    public Vector2 NearestEnemy()
    {
        Vector2 dir = Vector2.zero;
        float dist = detectionRange;
        enemies = Physics2D.CircleCastAll(transform.position, detectionRange, transform.up, 0f);

        //Debug.Log("enemies: " + enemies.Length);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].collider.gameObject.layer == 6)
            {
                RaycastHit2D enemyCollision = enemies[i];
                if (enemyCollision.distance < dist)
                {
                    dir = enemyCollision.collider.transform.parent.position;
                }
            }
        }

        if (dir == Vector2.zero)
        {
            IsAttacking = false;
        } else IsAttacking = true;

        return dir;
    }

    private void Attack()
    {
        meleeAttack.StartAttack(NearestEnemy());
    }
}
