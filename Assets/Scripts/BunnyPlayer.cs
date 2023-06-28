using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BunnyPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    Transform tr;
    Animator anim;
    LogicScript logic;
    MusicScript music;
    AudioSource source;

    public AudioClip land;
    
    public AudioClip jump1;
    public AudioClip jump2;
    public AudioClip pickup;
    public AudioClip death;

    public float speed;
    public float jumpShortSpeed;
    public float jumpSpeed;
    bool dead = false;
    bool jump = false;
    bool jumpCancel = false;

    public int direction = 1;
    
    public SceneManager sceneManager = new();

    public bool isColliding = false;
    public bool isGrounded = false;
    public bool jumpedOnce = false;
    public bool jumpedTwice = false;

   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;

        if (collision.gameObject.CompareTag("Carrot"))
        {
            collision.gameObject.GetComponent<CarrotScript>().PickUp();
            logic.IncrementCarrots();
            source.PlayOneShot(pickup);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded) source.PlayOneShot(land);

            isGrounded = true;
            jumpedOnce = false;
            jumpedTwice = false;
        }


        if(collision.gameObject.CompareTag("Spikes"))
        {
            music.StopMusic();
            source.PlayOneShot(death);
            dead = true;
            anim.SetBool("dead", true);
            logic.DisplayDeathText();

        }
    }
    


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        tr = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicScript>(); 
        source = gameObject.GetComponent<AudioSource>();
    }


    void Update()
    {
        isColliding = false;

        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("jumpedTwice", jumpedTwice);

        if (!dead)
        {
            if (rb.velocity.x != 0)
            {
                if (rb.velocity.x > 0) direction = 1;
                else direction = -1;
            }

            if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || !jumpedTwice))
            {
                jump = true;
            }
            if (Input.GetKeyUp(KeyCode.Space) && (!isGrounded))
            {
                jumpCancel = true;
            }

            tr.localScale = new Vector3(Mathf.Abs(tr.localScale.x) * direction, tr.localScale.y, tr.localScale.z);
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                music.PlayMusic();
                SceneManager.LoadScene("Game");
            }
        }
        
    }

    void FixedUpdate()
    {
        if(dead) return;

        float hori = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hori * speed, rb.velocity.y);

        if (jump) {
            if (jumpedOnce)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * 0.75f);
                source.PlayOneShot(jump1);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                source.PlayOneShot(jump2);
            }
            isGrounded = false;

            if (jumpedOnce) jumpedTwice = true;
            else jumpedOnce = true;
            jump =false;
        }

        if(jumpCancel)
        {
            if (rb.velocity.y > jumpShortSpeed)
                rb.velocity = new Vector2(rb.velocity.x, jumpShortSpeed);
            jumpCancel = false;
        }
    }
}
