using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    You should add destroyer script to each prefab/generated object for correct work.
    This script will destruct every object which go in out screen in left side.
*/
public class LastGeneratedObjectDestroyer : MonoBehaviour
{
    private GameObject platformDestructionPoint;

    void Start()
    {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
    }

    void Update()
    {
        destroyLastPlatform();
    }

    private void destroyLastPlatform()
    {
        if (transform.position.x < platformDestructionPoint.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
