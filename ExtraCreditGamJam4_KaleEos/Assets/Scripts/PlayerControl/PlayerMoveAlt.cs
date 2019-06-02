using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAlt : MonoBehaviour
{
    AudioManager audioManager;

    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Vector3 startingPosition;    // starting position
    public float deathHeight;           // deadzone position

    void Start()
    {
        audioManager = AudioManager.instance;               // instantiates AudioManager

        if (audioManager == null)
        {
            Debug.LogError("No AudioManager Found");
        }

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Update()
    {
        // jumping conditions
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }

            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        // restart conditions
        if (this.gameObject.transform.position.y <= deathHeight)
        {
            //if (scenemanager.scenecountinbuildsettings = 1)
            //{
            //    debug.log("done");
            //}
            // audioManager.PlaySound("");
            Respawn();
        }

        if (this.gameObject.transform.position.y >= deathHeight && Input.GetKeyDown("r"))
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
}