using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsSetter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.SetInt("coins", 0);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.SetInt("coins", 60);
        }
    }
}
