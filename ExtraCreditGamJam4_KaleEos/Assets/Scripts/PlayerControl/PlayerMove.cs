using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    AudioManager audioManager;
    public Animator idle;

    public float speed;                     // custom speed var
    private float moveInput;                // left-right input
    private Rigidbody2D rb;
    private bool facingRight = true;

    [Space]
    [Header("Jump Control:")]
    public float jumpForce;                 // jump power
    private int extraJumps;                 // more jumps
    public int extraJumpsValue;             // actual value

    [Space]
    [Header("Ground Control:")]
    private bool isGrounded;                // yes-no if grounded
    public Transform groundCheck;           // ground object
    public float checkRadius;               // check distance;
    public LayerMask whatIsGround;          // tag check

    [Space]
    [Header("Positions:")]
    public Vector3 startingPosition;        // spawn location
    public float deathHeight;               // deadzone position
    public GameObject EditUI;               // In-game editor UI mode

    [Space]
    [Header("Time Controls:")]
    public bool cooldownState;              // state for cooldown
    public float powerDuration = 2f;        // how long can slow time + 1 second
    public float cooldownDuration = 5f;     // must change where this is + 1 of powerDuration
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
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // jump condtions
        if(isGrounded == true)              // gives jumps
        {
            extraJumps = extraJumpsValue;
        }

        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)       // jump
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)       // no more jumps
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        // restart conditions
        if(this.gameObject.transform.position.y <= deathHeight)         // falls below death zone = restart
        {
            //if (scenemanager.scenecountinbuildsettings = 1)
            //{
            //    debug.log("done");
            //}
            // audioManager.PlaySound("");
            Respawn();
        }

        if(this.gameObject.transform.position.y >= deathHeight && Input.GetKeyDown("r"))        // manual restart
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

            if (cooldownStart >= cooldownDuration)      // metered time
            {
                cooldownState = false;
                cooldownStart = 0f;
                Time.timeScale = 1f;
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
        this.gameObject.transform.position = startingPosition;          // restarts position to starting position. No checkpoints.
    }

    void FixedUpdate()
    {
        // Ground check conditions
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // movement conditions
        moveInput = Input.GetAxis("Horizontal");

        if (moveInput >= 0)
        {
            idle.SetFloat("ToRun", 2);
        }
        else if (moveInput == 0)
        {
            idle.SetFloat("ToRun", 0);
        }

        // Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();                                                 // for sprites
            // transform.eulerAngles = new Vector3(0, 0, 0);        // for bone structure
        }

        else if (facingRight == true && moveInput < 0)
        {
            Flip();                                                 // for sprites
            // transform.eulerAngles = new Vector3(0, 180, 0);      // for bone structure
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