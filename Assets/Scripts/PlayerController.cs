using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent<Vector2> movementEvent;
    public UnityEvent dashEvent;

    [SerializeField] float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (movementEvent != null)
        {
            movementEvent = new UnityEvent<Vector2>();
        }
        movementEvent.AddListener(Move);
        dashEvent.AddListener(Dash);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move(Vector2 moveVector)
    {
        Vector2 dir = new Vector2(transform.position.x, transform.position.y) + moveVector.normalized;
        Debug.Log($"Moving: {moveVector}");
        transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        transform.position = Vector2.Lerp(transform.position, dir, speed * Time.deltaTime);
    }

    void Dash()
    {
        Debug.Log("Dash");
        transform.position += transform.up * 2;
    }
}
