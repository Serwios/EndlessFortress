using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public LayerMask coinLayer;

    private Collider2D myCollider;

    private Animator myAnimator;

    private bool isDashing;
    public float dashtime;
    public float dashSpeed;
    public float dashCoolDown;

    private float dashTimeLeft;
    private float lastDash = -100;

    private Vector3 playerPosition;
    private bool isTouchingCoin;

    public static int numOfCollectedCoins;

    void Start()
    {
        numOfCollectedCoins = 0;

        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        checkPossibilityToJump();
        checkPossibilityToDash();
        checkPossibilityToDie();
        checkPossibilityToTakeCoin();
        checkPossibilityToExit();
    }

    private void checkPossibilityToDash()
    {
        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                AttemptToDash();
            }
        }

        CheckDash();
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashtime > 0)
            {
                myRigidbody.velocity = new Vector2(dashSpeed, myRigidbody.velocity.y);
                dashTimeLeft -= Time.deltaTime;
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashtime;
        lastDash = Time.time;
    }

    private void checkPossibilityToJump()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }

    private void checkPossibilityToDie()
    {
        playerPosition = GameObject.Find("Player").transform.position;

        if (playerPosition.y < -12.5)
        {
            SceneManager.LoadScene("RunningScene");
        }
    }

    private void checkPossibilityToTakeCoin()
    {
        isTouchingCoin = Physics2D.IsTouchingLayers(myCollider, coinLayer);
        if (isTouchingCoin)
        {
            numOfCollectedCoins++;
        }

        //Debug.Log("numOfCoins: " + numOfCollectedCoins);
    }

    private void checkPossibilityToExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}