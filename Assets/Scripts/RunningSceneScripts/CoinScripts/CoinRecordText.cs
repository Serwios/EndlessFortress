using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.UI;
/*
    @recordText - recordText UI for visualisation
*/
public class CoinRecordText : MonoBehaviour
{
    public Text recordText;
    private int coins;

    void Start()
    {
        coins = PlayerPrefs.GetInt("coins") != null ? PlayerPrefs.GetInt("coins") : 0;
    }

    void Update()
    {
        recordText.text = "Record: " + coins.ToString();
    }
}
