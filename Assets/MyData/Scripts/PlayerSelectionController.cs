using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
//using CompleteProject;

public class PlayerSelectionController : MonoBehaviour
{

    public GameObject[] Players;
    public Button[] PlayersBtns;
    public string[] PlayersName;
    public int[] PlayersPrice;
    public Image RangeSlider, DamageSlider, FireRateSlider, ScopeSlider;
    public float[] Range, Damage, FireRate, Scope;
    public GameObject AreYouSurePanel, NotEnoughPanel, LockImage, BuyBtn, SelectBtn;
    public Text PlayerPriceText, PlayerNameText;
    public GameObject[] HighLighters;

    public static PlayerSelectionController instance;
    //public GameObject PlayersObject;

    int pointer;

    void OnEnable()
    {
        instance = this;
        Players[0].transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].SetActive(false);
            HighLighters[i].SetActive(false);
        }
        PlayerPrefs.SetInt("PlayerUnlocked" + 0, 1);

        RangeSlider.fillAmount = Range[PlayerPrefs.GetInt("SelectedPlayer")] / 100f;
        DamageSlider.fillAmount = Damage[PlayerPrefs.GetInt("SelectedPlayer")] / 100f;
        FireRateSlider.fillAmount = FireRate[PlayerPrefs.GetInt("SelectedPlayer")] / 100f;
        ScopeSlider.fillAmount = Scope[PlayerPrefs.GetInt("SelectedPlayer")] / 100f;

        BuyBtn.SetActive(false);
        LockImage.SetActive(false);
        Players[PlayerPrefs.GetInt("SelectedPlayer")].transform.parent.gameObject.SetActive(true);
        PlayersBtns[PlayerPrefs.GetInt("SelectedPlayer")].Select();
        PlayersBtns[PlayerPrefs.GetInt("SelectedPlayer")].OnSelect(null);
        PlayerPriceText.transform.parent.gameObject.SetActive(false);

        Players[PlayerPrefs.GetInt("SelectedPlayer")].SetActive(true);
        HighLighters[PlayerPrefs.GetInt("SelectedPlayer")].SetActive(true);
        PlayerNameText.text = PlayersName[PlayerPrefs.GetInt("SelectedPlayer")];
    }
    public void PlayersBtn(int index)
    {
        pointer = index;
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].SetActive(false);
            HighLighters[i].SetActive(false);
        }
        Players[index].SetActive(true);
        HighLighters[index].SetActive(true);

        PlayerNameText.text = PlayersName[index];

        RangeSlider.fillAmount = Range[index] / 100f;
        DamageSlider.fillAmount = Damage[index] / 100f;
        FireRateSlider.fillAmount = FireRate[index] / 100f;
        ScopeSlider.fillAmount = Scope[index] / 100f;

        if (PlayerPrefs.GetInt("PlayerUnlocked" + index) == 1)
        {
            LockImage.SetActive(false);
            BuyBtn.SetActive(false);
            //SelectBtn.SetActive(true);
            PlayerPriceText.transform.parent.gameObject.SetActive(false);
            PlayerPrefs.SetInt("SelectedPlayer", index);
        }
        else
        {
            LockImage.SetActive(true);
            BuyBtn.SetActive(true);
            //SelectBtn.SetActive(false);
            PlayerPriceText.transform.parent.gameObject.SetActive(true);
            PlayerPriceText.text = PlayersPrice[index].ToString();
        }
    }
    public void Buy()
    {
        Players[0].transform.parent.gameObject.SetActive(false);
        MainMenuController.instance.ClickEffect();
        if (PlayersPrice[pointer] <= PlayerPrefs.GetInt("TotalCash"))
        {
            AreYouSurePanel.SetActive(true);
        }
        else
        {
            NotEnoughPanel.SetActive(true);
            //for (int i = 0; i < BuyNowBtns.Length; i++)
            //{
            //    BuyNowBtns[i].SetActive(false);
            //}
            //BuyNowBtns[pointer - 1].SetActive(true);
        }
    }
    public void BuyNow()
    {
        MainMenuController.instance.ClickEffect();
        Players[0].transform.parent.gameObject.SetActive(true);
        AreYouSurePanel.SetActive(false);
        PlayerPrefs.SetInt("PlayerUnlocked" + pointer, 1);
        PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") - PlayersPrice[pointer]);
        MainMenuController.instance.UpdateCashTexts();
        //TotalCashText.text = PlayerPrefs.GetInt ("TotalCash").ToString ();
        BuyBtn.SetActive(false);
        //SelectBtn.SetActive(true);
        LockImage.SetActive(false);

        PlayerPriceText.transform.parent.gameObject.SetActive(false);
        PlayerPrefs.SetInt("SelectedPlayer", pointer);
    }
    public void OK()
    {
        Players[0].transform.parent.gameObject.SetActive(true);
        AreYouSurePanel.SetActive(false);
        NotEnoughPanel.SetActive(false);
    }
    public void NewPlayerUnlockedCongo()
    {
        MainMenuController.instance.ClickEffect();
        NotEnoughPanel.SetActive(false);
        Players[0].transform.parent.gameObject.SetActive(true);
        BuyBtn.SetActive(false);
        LockImage.SetActive(false);
        PlayerPriceText.transform.parent.gameObject.SetActive(false);
        PlayerPrefs.SetInt("SelectedPlayer", pointer);
    }
    public void InAppNow()
    {
        //Purchaser.instance.BuyPlayers(pointer);
    }
    private void OnDisable()
    {
        if(Players[0] != null)
            Players[0].transform.parent.gameObject.SetActive(false);
    }
}
