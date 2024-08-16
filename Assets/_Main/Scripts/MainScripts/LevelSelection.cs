using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [Header("No of Levels to be Played by user")]
    [SerializeField]
    private GameObject[] Levels;
    [Header("Loading Screen for Loading Level")]
    [SerializeField]
    private GameObject LoadingScreen; 
   


    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("ALL_Levels_Buy")==1)
        {
            UnloackAllLevels();
        }
        for (int i = 0; i < Levels.Length; i++)
        {
            if (i <= PlayerPrefs.GetInt("Levels"))
            {
                Levels[i].gameObject.transform.Find("Lock").gameObject.SetActive(false);
            }
            else
            {
                Levels[i].gameObject.transform.Find("Lock").gameObject.SetActive(true);
            }
           
        }
      
      

    }
    public void SelectLevels(int Level_Num)
    {
        FindObjectOfType<MainMenuSound>().BtnSoundPlay();
        
       
        LoadingScreen.SetActive(true);
        // FindObjectOfType<Loading>().scen_index=PlayerPrefs.GetInt("Mode_no");
        FindObjectOfType<Loading>().scen_index = 1;
        PlayerPrefs.SetInt("Level_No", Level_Num);
        Analytics.CustomEvent("GamePlay", new Dictionary<string, object>
            {
                { "Level Select by User", PlayerPrefs.GetInt("Level_No")}
            });
    }
    public void UnloackAllLevels()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].gameObject.transform.Find("Lock").gameObject.SetActive(false);

        }
       
    }
    public void BtnSound()
    {
        FindObjectOfType<MainMenuSound>().BtnSoundPlay();
    }
}
