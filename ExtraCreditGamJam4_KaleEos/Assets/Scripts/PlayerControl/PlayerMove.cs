using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    AudioManager audioManager;
    
    public float speed;                         // custom speed var
    public float jumpForce;                     // jump power
    private float moveInput;                    // left-right input

    private Rigidbody2D rb;
    private bool facingRight = true;

    private bool isGrounded;                    // yes-no if grounded
    public Transform groundCheck;               // ground object
    public float checkRadius;                   // check distance;
    public LayerMask whatIsGround;              // tag check

    private int extraJumps;
    public int extraJumpsValue;

    public Vector3 startingPosition;            // spawn location
    public float deathHeight;                   // deadzone position

    public bool thisDoesNothing = false;        // yup
    
    void Start()
    {
        audioManager = AudioManager.instance;               // instantiates AudioManager

        if (audioManager == null)
        {
            Debug.LogError("No AudioManager Found");
        }

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // jump condtions
        if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // restart conditions
        if(this.gameObject.transform.position.y <= deathHeight)
        {
            //if (scenemanager.scenecountinbuildsettings = 1)
            //{
            //    debug.log("done");
            //}
            // audioManager.PlaySound("");
            Respawn();
        }

        if(this.gameObject.transform.position.y >= deathHeight && Input.GetKeyDown("r"))
        {
            Respawn();
            Debug.Log("response");
        }

        //if (Input.GetKeyDown("r"))    // use for debug
        //{
        //    Respawn();
        //}
    }

    public void Respawn()
    {
        this.gameObject.transform.position = startingPosition;
    }

    void FixedUpdate()
    {
        // Ground check conditions
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            // Flip();                                                 // for sprites
            transform.eulerAngles = new Vector3(0, 0, 0);        // for bone structure
        }

        else if (facingRight == true && moveInput < 0)
        {
            // Flip();                                                 // for sprites
            transform.eulerAngles = new Vector3(0, 180, 0);      // for bone structure
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}