using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsSetter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.SetInt("coins", 0);
        }
    }
}
