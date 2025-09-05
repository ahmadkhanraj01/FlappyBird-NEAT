using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex = 0;
    public float gravity = -9.81f;
    public float strength = 5f;
    
    // Cache GameManager reference
    private GameManager gameManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Cache the GameManager reference once
        gameManager = FindObjectOfType<GameManager>();
    }
    
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }
    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }
    
    private void Update()
    {
        // Keyboard (Space / Jump)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            direction = Vector3.up * strength;
        }

        // Mouse click (PC testing)
        if (Input.GetMouseButtonDown(0)) // 0 = left click
        {
            direction = Vector3.up * strength;
        }

        // Touch (Mobile devices)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }

        // Apply gravity
        direction.y += gravity * Time.deltaTime;

        // Move player
        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = sprites[spriteIndex];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Use cached reference instead of FindObjectOfType
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        }
        else if (other.gameObject.CompareTag("Scoring"))
        {
            gameManager.IncreaseScore();
        }
    }
}