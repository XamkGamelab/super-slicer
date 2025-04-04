using UnityEngine;
using UnityEngine.UI;

public class Shuriken : MonoBehaviour
{
    private GameManager gameManager;
    public Vector3 direction = Vector3.up;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float lifetime;
    [SerializeField] RectTransform sprite;
    float rotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.Instance;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        rotation -= Time.deltaTime * rotationSpeed;
        sprite.rotation = Quaternion.Euler(0f, 0f, rotation);

        transform.position += direction * speed * Time.deltaTime;
    }
}
