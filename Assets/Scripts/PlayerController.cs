using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isActive = true;

    [Header("Audio")]
    public AudioClip milestoneSound;
    private AudioSource audioSource;

    [Header("Visual")]
    public SpriteRenderer spriteRenderer;
    public Color activeColor = Color.white;
    public Color inactiveColor = Color.gray;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Setup audio
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isActive && isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        UpdateVisuals();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    void Jump()
    {
        PlayJumpSound();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }

    void UpdateVisuals()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = isActive ? activeColor : inactiveColor;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }

    void PlayJumpSound()
    {
        if (milestoneSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(milestoneSound);
        }
    }
}