using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoveAlt : MonoBehaviour
{
    AudioManager audioManager;
    public Animator animator; 

    private Rigidbody2D rb;
    public float speed;
    private float moveInput;
    private bool facingRight = true;

    [Space]
    [Header("Jump Control:")]
    public float jumpForce;             // jump power
    private float jumpTimeCounter;      // in seconds
    public float jumpTime;
    private bool isJumping;             // in air detector

    [Space]
    [Header("Ground Control:")]
    public bool isGrounded;
    public Transform groundCheck;       // ground detector
    public float checkRadius;
    public LayerMask whatIsGround;      // ground verification

    [Space]
    [Header("Positions:")]
    public Vector3 startingPosition;    // starting position
    public float deathHeight;           // deadzone position
    public GameObject EditUI;           // In-game editor UI mode

    [Space]
    [Header("Time Controls:")]
    public bool cooldownState;              // state for cooldown
    public float powerDuration = 2f;        // how long can slow time + 1 second
    public float cooldownDuration = 4f;     // must change where this is + 1 of powerDuration
    private float cooldownStart = 0f;       // starting point for cooldown timer

    public bool thisDoesNothing = false;        // yup

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
        // Direction Modifier
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        animator.SetFloat("SpeedAnim", Mathf.Abs(moveInput));

        if (facingRight = false && moveInput > 0)
        {
            Flip();
            // transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (facingRight = true && moveInput < 0)
        {
            Flip();
            // transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Update()
    {

        // Jump Conditions
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))      // if grounded, jump is good to go
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if(jumpTimeCounter > 0)                         // allows for jump if jumpis remainingg
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }

            else
            {
                isJumping = false;              // no jump for you
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        // Restart Conditions
        if (this.gameObject.transform.position.y <= deathHeight)        // falls below death zone = restart
        {
            //if (scenemanager.scenecountinbuildsettings = 1)
            //{
            //    debug.log("done");
            //}
            // audioManager.PlaySound("");
            Respawn();
        }

        if (this.gameObject.transform.position.y >= deathHeight && Input.GetKeyDown("r"))   // manual restart
        {
            Respawn();
            Debug.Log("response");
        }

        //if (Input.GetKeyDown("r"))    // use for debug
        //{
        //    Respawn();
        //}

        // Freezing Conditions and Cooldown
        if (Input.GetKey(KeyCode.W) && cooldownState == false)          // activates slow down time
        {
            cooldownState = true;
            Time.timeScale = 0.4f;
            //StartCoroutine(EditTimer());
        }
        else if (Input.GetKey(KeyCode.W) && cooldownState == true)      // prevents spam of slow down time
        {
            Debug.Log("Does Nothing");
        }

        // Manual Turn Off Control
        if (Input.GetKey(KeyCode.E) && cooldownState == true)           // manual control of turn off
        {
            cooldownState = false;
            Time.timeScale = 1f;
        }

        if (Input.GetKey(KeyCode.E) && cooldownState == false)          // prevents glitch out
        {
            Debug.Log("Does Nothing");
        }

        // Cooldown Control
        if (cooldownState == true)                      // condition for cooldown
        {
            cooldownStart += Time.deltaTime;

            if(cooldownStart >= cooldownDuration)       // metered time
            {
                cooldownState = false;
                cooldownStart = 0f;
            }            
        }
    }

    IEnumerator EditTimer()
    {
        yield return new WaitForSeconds(powerDuration);
        Time.timeScale = 1f;
    //    EditUI.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        this.gameObject.transform.position = startingPosition;      // restarts position to starting position. No checkpoints.
    }
}