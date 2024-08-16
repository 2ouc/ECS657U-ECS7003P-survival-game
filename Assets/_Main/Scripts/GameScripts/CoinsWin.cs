using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsWin : MonoBehaviour
{
    [Header("List of Coins by Levels")]
    [SerializeField]
    private int[] Level_Coins;


    public static CoinsWin instance;
    private void Start()
    {
            instance=this;
      
    }

    public void RewardWin()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins")+ Level_Coins[PlayerPrefs.GetInt("Level_No")]);
        
    }

  
}
