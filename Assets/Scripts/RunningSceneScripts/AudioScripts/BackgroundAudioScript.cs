using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    Singleton background script for playing bg OST once time;
*/
public class BackgroundAudioScript : MonoBehaviour
{
    private static BackgroundAudioScript instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
