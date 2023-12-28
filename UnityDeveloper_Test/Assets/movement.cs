using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f, jump = 8.0f;
    bool isGrounded = false;
    Rigidbody rb;
    Animator anim;
    public GameObject leftHollow, rightHollow, upHollow, downHollow, environment, gameOver;
    // The Hollows are the different holograms. Environment is the level. GameOver is the "Game Over" text.
    public Transform camera; // For a third person camera, the player moves in the direction the camera points at
    int direction = 0;
    /*
    This tells which direction the gravity has to move in when enter is pressed. By default it is down
    0 = down
    1 = up
    2 = right
    3 = left
    */
    Quaternion newRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        // None of the holograms will be visible when the game begins
        leftHollow.SetActive (false);
        rightHollow.SetActive (false);
        upHollow.SetActive (false);
        downHollow.SetActive (false);
    }


    // Update is called once per frame
    void Update()
    {
        
        // Input handling for movement
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3 (horizontal, 0f, vertical).normalized;
        Vector3 cameraForward = camera.forward;
        Vector3 cameraRight = camera.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Make the character move where the camera is looking
        Vector3 forwardMovement = vertical * cameraForward;
        Vector3 rightMovement = horizontal * cameraRight;
        Vector3 movementDirection = forwardMovement + rightMovement;

        // Player movement. MovementDirection is the variable that decided how the player moves
        rb.velocity = new Vector3 (movementDirection.x * speed, GetComponent<Rigidbody>().velocity.y, movementDirection.z * speed);

        // Jumping
        if (Input.GetAxis ("Jump") != 0 && isGrounded == true)
        {
            rb.velocity = new Vector3 (GetComponent<Rigidbody>().velocity.x, jump, GetComponent<Rigidbody>().velocity.z);
            isGrounded = false;
        }

        // Pressing any of the arrow keys will make the hologram selection screen appear
        if (Input.GetButtonDown ("Up")) 
        {
            upHollow.SetActive (true);
            HologramMode();
        }
        if (Input.GetButtonDown ("Down")) 
        {
            downHollow.SetActive (true);
            HologramMode();
        } if (Input.GetButtonDown ("Left")) 
        {
            leftHollow.SetActive (true);
            HologramMode();
        } if (Input.GetButtonDown ("Right")) 
        {
            rightHollow.SetActive (true);
            HologramMode();
        }
        
        // Pressing Enter makes the active selection decide where the gravity goes
        if (Input.GetAxis ("Submit") != 0)
        {
            upHollow.SetActive (false);
            downHollow.SetActive (false);
            leftHollow.SetActive (false);
            rightHollow.SetActive (false);
            if (direction == 0) newRotation = Quaternion.Euler (0, 0, 0);
            else if (direction == 1) newRotation = Quaternion.Euler (0, 0, 180f);
            else if (direction == 2) newRotation = Quaternion.Euler (0, 0, 270f);
            else if (direction == 3) newRotation = Quaternion.Euler (0, 0, 90f);
            environment.transform.rotation = newRotation; // The level rotates in the direction
        }

        // For the animation
        if (rb.velocity.y < -2) anim.SetBool ("Fall", true);
        else anim.SetBool ("Fall", false);
        if (Input.GetAxis ("Horizontal") == 0 && Input.GetAxis ("Vertical") == 0)
        {
            anim.SetBool ("Idle", true);
            anim.SetBool ("Run", false);
        }
        else if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0)
        {
            anim.SetBool ("Idle", false);
            anim.SetBool ("Run", true);
        }
        Debug.Log (rb.velocity.y);
    }

    void OnCollisionEnter()
    {
        // To prevent the player from jumping infinitely
        isGrounded = true;
    }

    void HologramMode()
    {
        // For any of the arrow keys pressed, the hologram of that direction will exist and all others will disappear
        if (Input.GetButtonDown ("Up"))
        {
            upHollow.SetActive (true);
            downHollow.SetActive (false);
            leftHollow.SetActive (false);
            rightHollow.SetActive (false);
            direction = 1;
        } if (Input.GetButtonDown ("Down"))
        {
            upHollow.SetActive (false);
            downHollow.SetActive (true);
            leftHollow.SetActive (false);
            rightHollow.SetActive (false);
            direction = 0;
        } if (Input.GetButtonDown ("Left"))
        {
            upHollow.SetActive (false);
            downHollow.SetActive (false);
            leftHollow.SetActive (true);
            rightHollow.SetActive (false);
            direction = 3;
        } if (Input.GetButtonDown ("Right"))
        {
            upHollow.SetActive (false);
            downHollow.SetActive (false);
            leftHollow.SetActive (false);
            rightHollow.SetActive (true);
            direction = 2;
        }
    }
}