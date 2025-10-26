using UnityEngine;
using UnityEngine.SceneManagement;


    
public class KarkterKontrol : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    public float speed = 3.0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public bool Grounded = true;
    public float JumpForce=6.0f;
    private bool moving;
    public bool isJumping = false;
    private float movedirection;
    private bool Jump;
    private Scene scene;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (rigidbody.linearVelocity != Vector2.zero)
        {
            moving = true;

        }
        else
        {

            moving = false; 
        }
        rigidbody.linearVelocity = new Vector2(speed * movedirection,rigidbody.linearVelocityY);

        if(Jump==true)
        {

            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocityX, JumpForce);
            Jump=false;
        }
    }

    void Update()
    {
        if (Grounded == true && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.D))
            {
                movedirection = 1.0f;
                spriteRenderer.flipX = false;
                animator.SetFloat("speed", speed);

            }
            else if (Input.GetKey(KeyCode.A))
            {
                movedirection = -1.0f;
                spriteRenderer.flipX=true;
                animator.SetFloat("speed", speed);

            }
        
        
        }
        else if (Grounded==true)
        {
            movedirection=0.0f;
            animator.SetFloat("speed", 0.0f);
            animator.SetBool("isjumping", false);

        }

        if(Grounded==true && Input.GetKey(KeyCode.W))
        {
            Jump=true;
            Grounded = false;
            animator.SetBool("isjumping", true);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Zemin")
        {
            Grounded = true;
            animator.SetBool("isjumping", false);


        }
        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);


        }
        if (collision.gameObject.tag == "Respawn")
        {
            
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        }
    }
}
    