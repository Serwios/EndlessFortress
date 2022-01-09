using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine.UI;

public class CoinRecordText : MonoBehaviour
{
    public Text recordText;
    private PlayerData loadedData;
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
