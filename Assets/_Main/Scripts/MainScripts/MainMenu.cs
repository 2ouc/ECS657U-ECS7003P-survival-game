using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject GunsSelection, MainMenuPanel, MainMnu, ExitPanel, SettingPanel,UnlockallGunsPanel;
   



    private void Start()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetFloat("musicSlider")==0)
        {
            PlayerPrefs.SetFloat("musicSlider", 0.6f);
        }
        if (PlayerPrefs.GetFloat("soundSlider") ==0)
        {
            PlayerPrefs.SetFloat("soundSlider", 0.5f);
        }
        if (PlayerPrefs.GetInt("INAppBuy") == 0)
        {
            PlayerPrefs.SetFloat("soundSlider", 0.5f);
        }

    }

    public void PlayButton()
    {
        
        MainMenuPanel.SetActive(false);
        GunsSelection.SetActive(true);
        //UnlockallGunsPanel.SetActive(true);
      
       
    }

    public void Exit(bool check)
    {
       
        ExitPanel.SetActive(check);
        
    }
   public void Setting()
    {
       
        SettingPanel.SetActive(true);
        MainMnu.SetActive(false);
    }
    

    public void OpenPanel(GameObject gb)
    {
        gb.SetActive(true);
    }
    public void ClosePanel(GameObject gb)
    {
        gb.SetActive(false);
    }
    
    public void OpenURL(string URL)
    {
        Application.OpenURL(URL);
      //  FindObjectOfType<MainMenuSound>().BtnSoundPlay();
    }
    public void ButtonSound()
    {
        FindObjectOfType<MainMenuSound>().BtnSoundPlay();
    }
   
}

