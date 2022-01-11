using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    You should add destroyer script to each prefab/generated object for correct work.
    This script will destruct every object which will collise with @param - playerLayer
*/
public class ObjectByCollisionDestroyer : MonoBehaviour
{
    private bool isTouchingPlayer;
    private BoxCollider2D myCollider;

    public LayerMask playerLayer;

    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        checkIfPlayerTouching();
    }

    private void checkIfPlayerTouching()
    {
        isTouchingPlayer = Physics2D.IsTouchingLayers(myCollider, playerLayer);

        if (isTouchingPlayer)
        {
            Destroy(gameObject);
        }
    }
}