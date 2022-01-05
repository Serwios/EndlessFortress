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

    void Start()
    {
        string path = Application.streamingAssetsPath + "/characterInfo.json";
        loadedData = LoadMyData(path);
    }

    void Update()
    {
        recordText.text = "Record: " + loadedData.coins.ToString();
    }

    public PlayerData LoadMyData(string pathToDataFile)
    {
        string jsonString = File.ReadAllText(pathToDataFile);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonString);
        return playerData;
    }
}
