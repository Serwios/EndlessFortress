using UnityEngine;
using UnityEngine.UI;
/* 
    @coinstText - text UI for visualisation
*/
public class CoinsText : MonoBehaviour
{
    public Text coinsText;

    void Update()
    {
        coinsText.text = PlayerController.numOfCollectedCoins.ToString();
    }
}
