using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DailyRewardController : MonoBehaviour
{
    public GameObject CongratulationsPanel;
    public Button[] DailyRewardBtns;
    public GameObject[] Claim, Claimed;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < DailyRewardBtns.Length; i++)
        {
            if (i < PlayerPrefs.GetInt("ClaimedDays"))
            {
                Claimed[i].SetActive(true);
                Claim[i].SetActive(false);
                DailyRewardBtns[i].interactable = false;
            }
            else if(i == PlayerPrefs.GetInt("ClaimedDays"))
            {
                Claim[i].SetActive(true);
                Claimed[i].SetActive(false);
                DailyRewardBtns[i].interactable = true;
            }
            else
            {
                Claim[i].SetActive(false);
                Claimed[i].SetActive(false);
                DailyRewardBtns[i].interactable = false;
            }
        }
    }
    public void RewardAddBtnAction(int reward)
    {
        MainMenuController.instance.ClickEffect();
        PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") + reward);
        CongratulationsPanel.SetActive(true);
    }
    public void OK()
    {
        PlayerPrefs.SetString("sysString", System.DateTime.Now.ToBinary().ToString());
        PlayerPrefs.SetInt("ClaimedDays",PlayerPrefs.GetInt("ClaimedDays")+1);
        if(PlayerPrefs.GetInt("ClaimedDays") == 7)
        {
            PlayerPrefs.SetInt("ClaimedDays", 0);
        }
        MainMenuController.instance.ClickEffect();
        CongratulationsPanel.SetActive(false);
        gameObject.SetActive(false);
        MainMenuController.instance.MainMenuPanel.SetActive(true);

        
        MainMenuController.instance.UpdateCashTexts();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
