using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour
{
    [SerializeField]
    private Text CoinsText;
   
    private void OnEnable()
    {
        CoinsText.text = "" + PlayerPrefs.GetInt("TotalCash");
    }
    public void UpdateUI()
    {
        CoinsText.text = ""+ PlayerPrefs.GetInt("TotalCash");
      
    }
    public void RewardedCoins(int Coins)
    {

        PlayerPrefs.SetInt("TotalCash", Coins + PlayerPrefs.GetInt("TotalCash"));
        CoinsText.text = "" + PlayerPrefs.GetInt("TotalCash");


    }
    private void Update()
    {
        CoinsText.text = PlayerPrefs.GetInt("TotalCash").ToString();
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        PlayerPrefs.SetInt("Coins", 500 + PlayerPrefs.GetInt("TotalCash"));
    //        CoinsText.text = "" + PlayerPrefs.GetInt("v");

    //    }
    //}
}
