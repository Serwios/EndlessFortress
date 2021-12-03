using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {
	private bool isTouchingPlayer;
	private Collider2D myCollider;
	
	public LayerMask playerLayer;

	void Start() {
		myCollider = GetComponent<Collider2D>();
	}

    void Update() {
        checkIfPlayerTouching();
    }

    private void checkIfPlayerTouching() {
    	isTouchingPlayer = Physics2D.IsTouchingLayers(myCollider, playerLayer);
    	if (isTouchingPlayer) {
    		Destroy(gameObject);
    	}
    }
}
