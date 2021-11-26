using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float moveSpeed;
	public float jumpForce;

	private Rigidbody2D myRigidbody;

	public bool grounded;
	public LayerMask whatIsGround;

	private Collider2D myCollider;

	private Animator myAnimator;

    private bool isDashing;
    public float dashtime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;
    private float dashTimeLeft;
    private float lastImageXposition;
    private float lastDash = -100;


    
    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update() {
    	grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
        	if(grounded) {
        		myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        	}
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);

        if (Input.GetButtonDown("Dash")) {
        	if(Time.time >= (lastDash + dashCoolDown) )
        	AttemptToDash();
        }

        CheckDash();
    }

    private void AttemptToDash() {
    	isDashing = true;
    	dashTimeLeft = dashtime;
    	lastDash = Time.time;

    	PlayerAfterImagePool.Instance.GetFromPool();
    	lastImageXposition = transform.position.x;
    }

    private void CheckDash() {
    	if(isDashing) {
    		if (dashtime > 0) {
    			myRigidbody.velocity = new Vector2(dashSpeed, myRigidbody.velocity.y);
    			dashTimeLeft -= Time.deltaTime;

    			if(Mathf.Abs(transform.position.x - lastImageXposition) > distanceBetweenImages) {
    				PlayerAfterImagePool.Instance.GetFromPool();
    				lastImageXposition = transform.position.x;
    			}
    		}

    		if(dashTimeLeft <= 0) {
    			isDashing = false;
    		}
    	}
    }


}
