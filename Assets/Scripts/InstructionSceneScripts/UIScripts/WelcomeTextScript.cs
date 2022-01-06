using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeTextScript : MonoBehaviour
{
    public Text welcomeTextUI;
    private int randomIndex;
    private List<string> welcomeTextList;

    void Start()
    {
        welcomeTextList = WelcomeTextConstants.welcomeTextsList;
        randomIndex = Random.Range(0, welcomeTextList.Count);
    }
    void Update()
    {
        welcomeTextUI.text = "[" + welcomeTextList[randomIndex] + "]";
    }
}
