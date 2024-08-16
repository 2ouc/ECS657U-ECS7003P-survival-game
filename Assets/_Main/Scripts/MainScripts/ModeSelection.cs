using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class ModeSelection : MonoBehaviour
{
 
    [Header("Panels for Mode Working")]
    [SerializeField]
    private GameObject GunSelection;
    [SerializeField]
    private GameObject LevelSelection;
    [SerializeField]
    private GameObject cf_Canvas;
    [Header("Data for Mode Progress")]
    [SerializeField]
    private int[] TotalLevelsinModes;
    [SerializeField]
    private Image[] ModesImageProgress;
    [SerializeField]
    private Text[] ModesTextProgress;
    private void OnEnable()
    {
        for (int i = 0; i < ModesTextProgress.Length; i++)
        {
            ModesTextProgress[i].text = "00%";
            ModesImageProgress[i].fillAmount = 0;
        }
        StartCoroutine(ModesLevelsProgress(0));
        StartCoroutine(ModesLevelsProgress(1));
        StartCoroutine(ModesLevelsProgress(2));
        StartCoroutine(ModesLevelsProgress(3));
        cf_Canvas.SetActive(true);
    }


    IEnumerator ModesLevelsProgress(int index)
    {

        float percentage = (float)(PlayerPrefs.GetInt("LevelsUnlocked" + index, 1) - 1) / (float)TotalLevelsinModes[index];

        Debug.Log("percentage is:" + percentage);
        for (float j = 0; j < percentage; j += Time.deltaTime)
        {
            yield return null;
            float temp = j * 100f;

            if (temp > 100)
            {

                temp = 100;
            }
            ModesTextProgress[index].text = string.Format("{00:00}%", (int)temp);
            ModesImageProgress[index].fillAmount = j;

        }
        
    }

    public void SelectMode(int mode)
    {
        PlayerPrefs.SetInt("ModeSelected", mode);

        LevelSelection.SetActive(true);
        gameObject.SetActive(false);
        Analytics.CustomEvent("Mode No", new Dictionary<string, object>
            {
                { "Mode Select by User", PlayerPrefs.GetInt("ModeSelected")}
            });



    }
    private void OnDisable()
    {
        if (cf_Canvas.activeInHierarchy)
        {
            cf_Canvas.SetActive(false);
            Debug.Log("cf_Canvas Off");
        }

    }
    public void BackButton()
    {
        GunSelection.SetActive(true);
        gameObject.SetActive(false);
    }
    public void BtnSound()
    {
        FindObjectOfType<MainMenuSound>().BtnSoundPlay();
    }
}
