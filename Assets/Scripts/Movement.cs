using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour

{
    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;
    public float airMovementMultiplier = 4f;
    public float fuelModifier = 10f;
    float groundDrag = 5f;
    float airDrag = 0.3f;
    public float jumpForce = 15f;
    public float boostForce = 75f;
    public float boostModifier = 75f;
    public float boostOrbForce = 200f;

    public float fov = 90f;
    private float fovZoom = 120f;

    public float gravity = 1f;

    private float boostTimeout = 0;

    float horizontalMovement;
    float verticalMovement;

    private bool beatenGame = false;

    bool isGrounded;
    [SerializeField] Transform groundCheck = null;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask groundNoJumpMask;
    [SerializeField] LayerMask boostMask;
    [SerializeField] LayerMask boostOrbMask;
    float groundDistance = 0.25f;

    [SerializeField] Transform orientation = null;

    [SerializeField] Image blackout = null;
    Color color;
    float fadeAmount;
    float fadeSpeed = 0.25f;

    RaycastHit slopeHit;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode fuelKey = KeyCode.LeftShift;
    [SerializeField] KeyCode pauseAudioKey = KeyCode.P;

    [SerializeField] Image bar = null;

    [SerializeField] Camera cam = null;

    [SerializeField] AudioSource music = null;
    bool pauseAudio = false;

    public float maxFuel = 1000f;
    private float fuel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        fuel = maxFuel;

        music.Play(0);

    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance, groundNoJumpMask);

        TakeInput();
        SetDrag();

        if (Input.GetKeyDown(jumpKey))
        {
            Jump();
        }

        bar.fillAmount = fuel / maxFuel;

        ResetFuel();

        if (Input.GetKey(fuelKey) && !isGrounded && fuel > 0)
        {
            UseFuel();
        }

        CheckVelocity();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        CheckBoost();
        CheckBoostOrb();

        if (boostTimeout > 0)
        {
            boostTimeout -= 1;
        }

        if (Input.GetKeyDown(pauseAudioKey))
        {
            print("toggling");
            PauseMusic();
        }

        CheckBeatGame();
        GameEnd();

    }

    void TakeInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.transform.forward * verticalMovement + orientation.transform.right * horizontalMovement;
    }

    void SetDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMovementMultiplier, ForceMode.Acceleration);
        }
        
    }

    void Jump()
    {
        if (isGrounded){
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position + new Vector3(0,4.4f,0), Vector3.down, out slopeHit, 1.1f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    void UseFuel()
    {

        rb.AddForce(orientation.transform.forward * fuelModifier * Time.deltaTime, ForceMode.Acceleration);
        fuel -= 100f * Time.deltaTime;

        
    }
    
    void ResetFuel()
    {
        if (isGrounded && fuel < maxFuel)
        {
            fuel += 3000 * Time.deltaTime;
        }
    }

    void CheckVelocity()
    {
        if (rb.velocity.magnitude > 60)
        {
            if (fov < fovZoom)
            {
                fov += 1;
                cam.fieldOfView = fov;
            }
        }
        else
        {
            if (fov > 90)
            {
                fov -= 0.5f;
                cam.fieldOfView = fov;
            }
        }
    }


    void CheckBoost()
    {
        if (Physics.CheckSphere(groundCheck.position, 0.1f, boostMask) && boostTimeout == 0)
        {
            fuel = maxFuel;
            rb.AddForce(orientation.transform.forward * boostModifier * 200f, ForceMode.Acceleration);
            rb.AddForce(transform.up * boostForce * 200f, ForceMode.Acceleration);

            
            boostTimeout = 30;
        }
    }
    void CheckBoostOrb()
    {
        if (Physics.CheckSphere(groundCheck.position, 0.3f, boostOrbMask) && boostTimeout == 0)
        {
            fuel = maxFuel;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * boostOrbForce * 180, ForceMode.Acceleration);

            boostTimeout = 30;

        }
    }

    void CheckBeatGame()
    {
        if (rb.position.y > 2350 && beatenGame == false)
        {
            beatenGame = true;
        }
    }

    void GameEnd()
    {
        if (beatenGame)
        {
            color = blackout.color;
            if (blackout.color.a < 1)
            {
            fadeAmount = color.a + (fadeSpeed * Time.deltaTime);
            blackout.color = new Color(color.r, color.g, color.b, fadeAmount);
            }
            else
            {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
                SceneManager.LoadScene("endGame");
            }
            
        }
    }

    void PauseMusic()
    {
        if (pauseAudio == false)
        {
            music.Pause();
            pauseAudio = true;
        }
        else
        {
            if (pauseAudio == true)
            {
                music.UnPause();
            pauseAudio = false;
            }
        }
    }

}
