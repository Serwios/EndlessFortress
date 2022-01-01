using UnityEngine;
using UnityEngine.UI;

public class CoinsText : MonoBehaviour
{
    
   	public Text coinsText;
   	public static int coins;

    void Update() {
        coinsText.text = PlayerController.numOfCollectedCoins.ToString();
    }
}
