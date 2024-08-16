using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class EpisodeSelectionController : MonoBehaviour
{
    public static EpisodeSelectionController Instance;
    public GameObject cf_Canvas;
    public Button[] LevelBtns;
    public GameObject[] rwdBtn;
    public GameObject[] Locked, LevelsText,LockRing,gainedStars,LevelArrow,Connector;
    int ForRing;

    private void Start()
    {
        Instance = this;
    }
    void OnEnable()
    {
        cf_Canvas.SetActive(true);
        refreshlevels();
        for (int i = 0; i < LevelBtns.Length; i++)
        {
            if (i <= PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")))
            {
                Locked[i].SetActive(false);
              
                LevelBtns[i].interactable = true;
                if (PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")) == i)
                {
                    LockRing[i].GetComponent<rotating>().enabled = true;
                    LevelArrow[i].GetComponent<rotating>().enabled = true;
                    for(int j = 0; j < i; j++)
                    {
                        Connector[j].SetActive(true);
                        Connector[j].GetComponent<Animator>().enabled = true;
                        gainedStars[j].SetActive(true);

                    }

                }
                else
                {
                    LockRing[i].GetComponent<rotating>().enabled = false;
                }
                
                
            }
            else
            {
              

                Locked[i].SetActive(true);
                LevelBtns[i].interactable = false;
                LockRing[i].GetComponent<Animator>().enabled = false;
            }

            
           
        }

        
       
        if (PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"))> 0)
        {
            for (int i=0;i< PlayerPrefs.GetInt("LevelsUnlocked") + PlayerPrefs.GetInt("ModeSelected");i++)
            {
         
            }
        }
    }
    public void refreshlevels()
    {
        for (int i = 0; i < LevelBtns.Length; i++)
        {
            if (i <= PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")))
            {
                Locked[i].SetActive(false);

                LevelBtns[i].interactable = true;
                if (PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")) == i)
                {
                    LockRing[i].GetComponent<rotating>().enabled = true;
                    LevelArrow[i].GetComponent<rotating>().enabled = true;


                }
                else
                {
                    LockRing[i].GetComponent<rotating>().enabled = false;
                }


            }
            else if (i == PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")) + 1)
            {
                Locked[i].SetActive(true);
                LevelBtns[i].interactable = false;
                LockRing[i].GetComponent<Animator>().enabled = false;
               // rwdBtn[i].SetActive(true);


            }
            else if (i > PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")) + 1)
            {
                Locked[i].SetActive(true);
                LevelBtns[i].interactable = false;
                LockRing[i].GetComponent<Animator>().enabled = false;
                rwdBtn[i].SetActive(false);

            }
        }
        for (int j = 0; j < 29; j++)
        {
            if (j <= PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")))
            {

                Connector[j].SetActive(true);
                Connector[j].GetComponent<Animator>().enabled = true;
                gainedStars[j].SetActive(true);
            }
            else
            {
                Connector[j].SetActive(false);
                Connector[j].GetComponent<Animator>().enabled = false;
                gainedStars[j].SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        
    }

    public void EpisodeSelect(int index)
    {

        MainMenuController.instance.ClickEffect();
        PlayerPrefs.SetInt("LevelNo", index);
        LoadingLevelController.instance.LoadLevel("Gameplay");

        if (PlayerPrefs.GetInt("ADSUNLOCK") == 0)
        {
           
            
        }
    }
}
