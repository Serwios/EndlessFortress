using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public float timeStart = 5;

    void Update()
    {
        timeStart -= Time.deltaTime;

        if (Mathf.Round(timeStart) == 0 || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("RunningScene");
        }
    }
}
