using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoveAlt : MonoBehaviour
{
    AudioManager audioManager;

    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;             // jump power
    private float moveInput;
    private bool facingRight = true;

    private float jumpTimeCounter;      // in seconds
    public float jumpTime;
    private bool isJumping;             // in air detector

    private bool isGrounded;
    public Transform groundCheck;       // ground detector
    public float checkRadius;
    public LayerMask whatIsGround;      // ground verification

    public Vector3 startingPosition;    // starting position
    public float deathHeight;           // deadzone position

    public GameObject EditUI;           // In-game editor UI mode
    public float powerDuration = 1;     // how long can slow time
    public float cooldownTime = 2;      // in seconds
    private float nextFireTime = 0;

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
        // direction modifier
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

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

        // jumping conditions
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

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

        // freezing conditions and cooldown
        //if (Time.deltaTime > nextFireTime)
        //{
            if (Input.GetKey(KeyCode.W) && EditUI.activeInHierarchy == false)
            {
                Time.timeScale = 0.4f;
                EditUI.gameObject.SetActive(true);

                if (EditUI.activeInHierarchy == true)
                {
                    StartCoroutine(EditTimer());
                }
            }
            else
            {
                Debug.Log("EditUI already open");
            }

            //Debug.Log("Cooldown activated");
            //nextFireTime = Time.deltaTime + cooldownTime;
        //}

        // turn off
        if (Input.GetKey(KeyCode.E) && EditUI.activeInHierarchy == true)
        {
            Time.timeScale = 1f;
            EditUI.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("EditUI already closed");
        }
    }

    IEnumerator EditTimer()
    {
        yield return new WaitForSeconds(powerDuration);
        Time.timeScale = 1f;
        EditUI.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        this.gameObject.transform.position = startingPosition;
    }
}