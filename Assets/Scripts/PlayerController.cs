using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Events")]
    public UnityEvent<Vector2> movementEvent;
    public UnityEvent dashEvent;

    [SerializeField] float speed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float dashCD;
    private float currentDashCD = 0.0f;
    private float currentDashTime = 0.0f;
    public bool dashing;
    private Rigidbody2D rb;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDashCD > 0.0f) { currentDashCD -= Time.deltaTime; }
    }

    void Move(Vector2 moveVector)
    {
        Vector2 dir = new Vector2(transform.position.x, transform.position.y) + moveVector.normalized;
        //Debug.Log($"Moving: {moveVector}");
        transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        transform.position = Vector2.Lerp(transform.position, dir, speed * Time.deltaTime);
    }

    void Dash()
    {
        Debug.Log("Dash");
        //transform.position = Vector2.Lerp()//+= transform.up * 2;
        if (!dashing && currentDashCD <= 0.0f)
        {
            StartCoroutine(Dashing());
        }
    }

    IEnumerator Dashing()
    {
        dashing = true;
        //rb.simulated = false;
        currentDashTime = 0.0f;
        currentDashCD = dashCD;
        
        while (currentDashTime < dashTime)
        {
            currentDashTime += Time.deltaTime;
            transform.position += transform.up * dashSpeed * Time.deltaTime;

            // add check if colliding with edges or enemies

            yield return null;
        }
        dashing = false;
        //rb.simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (dashing)
        {
            if (collision.gameObject.CompareTag("Entity"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
