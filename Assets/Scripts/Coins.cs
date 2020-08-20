using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int coin;
    public TextMeshProUGUI CoinUi;

    public int maxCoins;
    public TextMeshProUGUI MaxCoinsUi;
    
    // Start is called before the first frame update
    void Start()
    {
        coin = 0;
        CoinUi.text = coin.ToString();
        maxCoins = PlayerPrefs.GetInt("Coin");
        // PlayerPrefs.DeleteKey("Coins");
        //  maxCoins.text = coin.ToString();
        //maxCoins = PlayerPrefs.GetInt("Coin");

    }

  
    public void UpdateCoins()
    {
        int addcoins = maxCoins + coin;
        maxCoins = addcoins;
        MaxCoinsUi.text = maxCoins.ToString(); 

        PlayerPrefs.SetInt("Coin", maxCoins);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {

            coin++;
            CoinUi.text = coin.ToString();
            Destroy(other.gameObject);
            
        }


    }

}
