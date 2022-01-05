using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    You should add destroyer script to each prefab/generated object for correct work
*/
public class PlatformDestroyer : MonoBehaviour
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
