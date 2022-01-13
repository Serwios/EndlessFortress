using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.UI;

//This script describe player behaviour with environment and themselve.
public class PlayerController : MonoBehaviour
{
    private float moveSpeed;
    public float jumpForce;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public LayerMask coinLayer;
    public LayerMask redCrystalLayer;
    public LayerMask goldenCrystalLayer;
    public LayerMask greenLifeCrystalLayer;
    public LayerMask hellCrystalLayer;

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

    private Vector3 fp;
    private Vector3 lp;
    private float dragDistance;
    public static float timeStart;
    public static bool flagRedCrystalIsTaken = false;
    private bool flagGreenLifeCrystalIsTaken = false;

    void Start()
    {
        dragDistance = Screen.height * 15 / 100;
        numOfCollectedCoins = 0;
        moveSpeed = 10;
        timeStart = 3;

        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        moveCharacterStraightforward();
        controllSwipeMoving();

        checkPossibilityToDie();
        checkPossibilityToTakeCoin();

        checkPossibilityToTakeRedCrystal();
        checkPossibilityToTakeGoldenCrystal();
        checkPossibilityToTakeGreenLifeCrystal();
        checkPossibilityToTakeHellCrystal();

        checkPossibilityToExit();

        checkPossibilityToDashForKeyboard();
        checkPossibilityToJumpForKeyboard();

        startTimerIfRedCrystalIsTaken();
    }

    private void moveCharacterStraightforward()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
    }

    private void checkPossibilityToDashForKeyboard()
    {
        dashForKeyboard();
        CheckDash();

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }

    private void checkPossibilityToJumpForKeyboard()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }

    private void dashForKeyboard()
    {
        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                AttemptToDash();
            }
        }
    }

    private void controllSwipeMoving()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;

                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {
                        if ((lp.x > fp.x))
                        {
                            if (Time.time >= (lastDash + dashCoolDown))
                            {
                                //Right Swipe
                                AttemptToDash();
                            }

                        }
                        else
                        {
                            //Left swipe
                        }
                    }
                    else
                    {
                        if (lp.y > fp.y)
                        {
                            //Up swipe 
                        }
                        else
                        {
                            //Down swipe
                        }
                    }
                }
                else
                {
                    //Tap
                    makeJumpTheCharacter();
                }
            }
        }

        CheckDash();
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
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

    private void makeJumpTheCharacter()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if (grounded)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
    }

    private void checkPossibilityToDie()
    {
        playerPosition = GameObject.Find("Player").transform.position;

        if (playerPosition.y < -12.5)
        {
            if (flagGreenLifeCrystalIsTaken)
            {
                transform.position = new Vector3(transform.position.x, 2.3f);
                flagGreenLifeCrystalIsTaken = false;
                return;
            }
            else if (flagRedCrystalIsTaken)
            {
                timeStart = 3;
                flagRedCrystalIsTaken = false;
            }
            else
            {
                int coins = PlayerPrefs.GetInt("coins") != null ? PlayerPrefs.GetInt("coins") : 0;

                //new record
                if (coins < numOfCollectedCoins)
                {
                    coins = numOfCollectedCoins;
                    PlayerPrefs.SetInt("coins", coins);
                    SceneManager.LoadScene(SceneNamesScript.castleScene);
                }
                else
                {
                    coins = PlayerPrefs.GetInt("coins");
                    PlayerPrefs.SetInt("coins", coins);
                    SceneManager.LoadScene(SceneNamesScript.castleScene);
                }
            }
        }
    }

    private void checkPossibilityToTakeCoin()
    {
        isTouchingCoin = Physics2D.IsTouchingLayers(myCollider, coinLayer);
        if (isTouchingCoin)
        {
            numOfCollectedCoins++;
        }
    }

    private void checkPossibilityToTakeRedCrystal()
    {
        if (Physics2D.IsTouchingLayers(myCollider, redCrystalLayer))
        {
            //If after collision is default speed
            if (moveSpeed < 20)
            {
                moveSpeed *= 2;
                myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
                flagRedCrystalIsTaken = true;
            }
            //If after collision character is high speed
            else if (moveSpeed == 20)
            {
                timeStart = 3;
            }

        }

        //If time after taking is over
        if (timeStart == 0)
        {
            moveSpeed /= 2;
            timeStart = 3;
            flagRedCrystalIsTaken = false;
        }


        //Debug.Log("Time " + timeStart);
    }

    private void checkPossibilityToTakeGoldenCrystal()
    {
        if (Physics2D.IsTouchingLayers(myCollider, goldenCrystalLayer))
        {
            numOfCollectedCoins += 10;
        }
    }

    private void checkPossibilityToTakeGreenLifeCrystal()
    {
        if (Physics2D.IsTouchingLayers(myCollider, greenLifeCrystalLayer))
        {
            flagGreenLifeCrystalIsTaken = true;
        }
    }

    private void checkPossibilityToTakeHellCrystal()
    {
        if (Physics2D.IsTouchingLayers(myCollider, hellCrystalLayer))
        {
            SceneManager.LoadScene(SceneNamesScript.hellScene);
        }
    }

    private void startTimerIfRedCrystalIsTaken()
    {
        if (flagRedCrystalIsTaken == true)
        {
            timeStart -= Time.deltaTime;
            if (timeStart <= 0)
            {
                timeStart = 0;
                flagRedCrystalIsTaken = false;
                return;
            }
        }
    }

    private void checkPossibilityToExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}