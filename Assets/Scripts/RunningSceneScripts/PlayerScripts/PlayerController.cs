using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 10;
    public float jumpForce;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public LayerMask coinLayer;
    public LayerMask redCrystalLayer;

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

    void Start()
    {
        dragDistance = Screen.height * 15 / 100;
        numOfCollectedCoins = 0;

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
        checkPossibilityToTakeCrystal();
        checkPossibilityToExit();

        checkPossibilityToDashForKeyboard();
        checkPossibilityToJumpForKeyboard();
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
            PlayerData playerData = new PlayerData();
            playerData.charName = "externalName";

            string path = Application.streamingAssetsPath + "/characterInfo.json";
            PlayerData loadedData = LoadMyData(path);

            //New record
            if (loadedData.coins < numOfCollectedCoins)
            {
                playerData.coins = numOfCollectedCoins;
                SaveMyData(playerData);
                SceneManager.LoadScene("RunningScene");
            }
            else
            {
                playerData.coins = loadedData.coins;
                SaveMyData(playerData);
                SceneManager.LoadScene("RunningScene");
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

    private void checkPossibilityToTakeCrystal()
    {
        if (Physics2D.IsTouchingLayers(myCollider, redCrystalLayer))
        {
            moveSpeed *= 2;
            myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        }

        //else if (Physics2D.IsTouchingLayers(myCollider, greenCrystalLayer) {
        //
        //}
    }

    private void checkPossibilityToExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SaveMyData(PlayerData data)
    {
        string jsonString = JsonUtility.ToJson(data);
        string path = Application.streamingAssetsPath + "/characterInfo.json";
        File.WriteAllText(path, jsonString);
    }

    public PlayerData LoadMyData(string pathToDataFile)
    {
        string jsonString = File.ReadAllText(pathToDataFile);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonString);
        return playerData;
    }
}