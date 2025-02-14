using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

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
    private float currentDashCD = 0.0f;
    private float currentDashDistance = 0.0f;
    public bool dashing;
    private Rigidbody2D rb;
    LayerMask mask;

    [SerializeField] GameObject meleeAttack;

    // TODO: Track closest enemy and attack that dir
    // also implement hitbox damage

    bool attacking = true;
    int health = 3;

    public int Health { get; set; }
    bool isDead = false;
    public bool IsAttacking { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        if (movementEvent != null)
        {
            movementEvent = new UnityEvent<Vector2>();
        }
        movementEvent.AddListener(Move);
        dashEvent.AddListener(Dash);
        rb = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Level", "Enemy");
        Health = health;
        IsAttacking = attacking;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDashCD > 0.0f) { currentDashCD -= Time.deltaTime; }

        meleeAttack.SetActive(IsAttacking);
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
                    Destroy(hit.collider.gameObject);
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
        gameManager.GameOver();
    }
}
