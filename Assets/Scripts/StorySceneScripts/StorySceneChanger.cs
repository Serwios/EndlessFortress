using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySceneChanger : MonoBehaviour
{
    public float timeStart = 15;

    void Update()
    {
        timeStart -= Time.deltaTime;

        if (Mathf.Round(timeStart) == 0 || Input.GetKeyDown(KeyCode.Space) || Input.touchCount == 1)
        {
            SceneManager.LoadScene(SceneNamesScript.instructionScene);
        }
    }
}
