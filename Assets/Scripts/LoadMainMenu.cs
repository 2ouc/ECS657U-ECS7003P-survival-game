using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainMenu : MonoBehaviour
{
    public GameObject ButtonAllReadyNext;
    public GameObject NewNext;
    private void OnEnable()
    {
        Debug.Log("LevelNo:"+PlayerPrefs.GetInt("LevelNo"));
        if (PlayerPrefs.GetInt("LevelNo")==29)
        {

            NewNext.SetActive(true);
            PlayerPrefs.SetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), 0) + 29);
            PlayerPrefs.SetInt("ShowModePanel",1);
        }
        else
        {
            NewNext.SetActive(false);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
