using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    Rigidbody2D rb2d;
    Vector2 movement = new Vector2();
    [SerializeField]
    bool grounded = false;
    
    SpriteRenderer spriteRenderer;
    Animator animator;
    SignalReceiver signalReceiver;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(x));
        if (x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {

            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }

        movement = new Vector2(x * speed, rb2d.velocity.y);

        animator.SetFloat("Fall", rb2d.velocity.y);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = movement;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }
    }
}
