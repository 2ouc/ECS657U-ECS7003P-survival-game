using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChapterSelectionController : MonoBehaviour
{
    public int[] ChapterTotalEpisodes;
    public Image[] ChapterImageProgress;
    public Text[] ChapterTextProgress;
    public Text[] ClearLEvels;
  //  public GameObject Character;
    public GameObject ModeTwoLock;
    public GameObject ModeTwoBtn;
    public GameObject ModeThreeLock;
    public GameObject ModeThreeBtn;
    public GameObject ModeFourLock;
    public GameObject ModeFourBtn;

    public GameObject cf_Canvas;

    // Start is called before the first frame update
    private void Start()
    {
        //(PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), 1) - 1);
       
        for (int i = 0; i < ChapterTextProgress.Length; i++)
        {
            ChapterTextProgress[i].text = "00%";
            ChapterImageProgress[i].fillAmount = 0;
        }

        StartCoroutine(ChapterProgressShower(0));
        StartCoroutine(ChapterProgressShower(1));
        StartCoroutine(ChapterProgressShower(2));
        StartCoroutine(ChapterProgressShower(3));
    }
    void OnEnable()
    {
        //PlayerPrefs.SetInt("LevelsUnlocked" + 0, 14);
        // PlayerPrefs.SetInt("LevelsUnlocked" + 1, 5);
        //PlayerPrefs.SetInt("LevelsUnlocked" + 2, 12);
        //PlayerPrefs.SetInt("LevelsUnlocked" + 3, 5);
        //if (PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")) >= 29)
        //{
        //    Debug.Log("Unlock Mode 2");
        //    ModeTwoLock.SetActive(false);
        //    ModeTwoBtn.GetComponent<Button>().interactable = true;

        //}

        // print("Level Mod 2 Unlocked : " + PlayerPrefs.GetInt("LevelsUnlocked" + 1));

        cf_Canvas.SetActive(true);
        Debug.Log("cf_Canvas ON");

        if (PlayerPrefs.GetInt("LevelsUnlocked" + 0) >= 29)
        {
            Debug.Log("Unlock Mode 2");
            ModeTwoLock.SetActive(false);
            ModeTwoBtn.GetComponent<Button>().interactable = true;

        }
        if (PlayerPrefs.GetInt("LevelsUnlocked" + 1) >= 29)
        {
            Debug.Log("Unlock Mode 3");
            ModeTwoLock.SetActive(false);
            ModeTwoBtn.GetComponent<Button>().interactable = true;
            ModeThreeLock.SetActive(false);
            ModeThreeBtn.GetComponent<Button>().interactable = true;

        }
        if (PlayerPrefs.GetInt("LevelsUnlocked" + 2) >= 29)
        {
            Debug.Log("Unlock Mode 4");
            ModeTwoLock.SetActive(false);
            ModeTwoBtn.GetComponent<Button>().interactable = true;
            ModeThreeLock.SetActive(false);
            ModeThreeBtn.GetComponent<Button>().interactable = true;
            ModeFourLock.SetActive(false);
            ModeFourBtn.GetComponent<Button>().interactable = true;

        }
        for (int i = 0; i < ChapterTextProgress.Length; i++)
        {
            ChapterTextProgress[i].text = "00%";
            ChapterImageProgress[i].fillAmount = 0;
            
        }

        StartCoroutine(ChapterProgressShower(0));
        StartCoroutine(ChapterProgressShower(1));
        StartCoroutine(ChapterProgressShower(2));
        StartCoroutine(ChapterProgressShower(3));

    }

    private void OnDisable()
    {
        if (cf_Canvas.activeInHierarchy)
        {
            cf_Canvas.SetActive(false);
            Debug.Log("cf_Canvas Off");
        }

    }

    IEnumerator ChapterProgressShower(int index)
    {

        float percentage = (float)(PlayerPrefs.GetInt("LevelsUnlocked"+index,1)-1) / (float)ChapterTotalEpisodes[index];
        
        Debug.Log("percentage is:"+percentage);
        for (float j = 0; j < percentage; j+=Time.deltaTime)
        {
            yield return null;
            float temp = j * 100f;
       
            if (temp > 100)
            {
                
                temp = 100;
            }
            ChapterTextProgress[index].text = string.Format("{00:00}%", (int)temp);
            ChapterImageProgress[index].fillAmount = j;

        } 
        //ChapterTextProgress[index].text = string.Format("{00:00}%", (int)(percentage * 100f));
        //ChapterImageProgress[index].fillAmount = percentage;
    }
    public void ModeBtnAction(int index)
    {
        MainMenuController.instance.ClickEffect();
        PlayerPrefs.SetInt("ModeSelected",index);
        Debug.Log("Selected Mode is:"+index);
        PlayerPrefs.SetInt("SelectedMode",index);
      //  Character.SetActive(false);
        Debug.Log("ModesBtnAction No" + PlayerPrefs.GetInt("SelectedMode"));

    }
}
