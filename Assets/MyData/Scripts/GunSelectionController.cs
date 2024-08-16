using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
//using CompleteProject;
//using UnityEditorInternal;

public class GunSelectionController : MonoBehaviour
{
    public GameObject[] Guns,LocksImg;
    public Button[] GunsBtns;
    public string[] GunsName;
    public int[] GunsPrice;
    public GameObject[] GunsForTexture;
    private Material[] GunsTexture;
    public Image RangeSlider, DamageSlider, FireRateSlider, ScopeSlider,AmmoRateImage;
    public Text DamageValue,Firerate,Mobility,Ammo,Rangetext;
    public float[] Range, Damage, FireRate, Scope,AmmoRate;
    public GameObject  /*LockImage,*/ BuyBtn,SelectBtn;
    public Text GunPriceText, GunNameText;
  //  public GameObject[] HighLighters;
    public static GunSelectionController instance;
    public GameObject PurchaseBtn, NotEnoughCoins;
    int pointer;
    //public GameObject[] GunsPanels;
    public Texture[] gunsTexture;
    List<GameObject> gunsParts = new List<GameObject>();
    GameObject[] PainPArts;
    public static int TobuyGunInt;
    static int PaintPartindex;
    private void Start()
    {
        
        
        //  PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash")+50000);
    }
    void OnEnable()
    {
        instance = this;
       // PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") + 50000000);
       
       PlayerPrefs.SetInt("GunUnlocked" + 0, 1);
        if (PlayerPrefs.GetInt("GunUnlocked" + 0) == 1)
        {
            PurchaseBtn.SetActive(false);
        }
       // Guns[0].transform.parent.gameObject.SetActive(true);

        StartCoroutine(SmoothSpecificationValueChange(RangeSlider, Range[PlayerPrefs.GetInt("SelectedGun")] / 100f));
        StartCoroutine(SmoothSpecificationValueChange(DamageSlider, Damage[PlayerPrefs.GetInt("SelectedGun")] / 100f));
        StartCoroutine(SmoothSpecificationValueChange(FireRateSlider, FireRate[PlayerPrefs.GetInt("SelectedGun")] / 100f));
        StartCoroutine(SmoothSpecificationValueChange(ScopeSlider, Scope[PlayerPrefs.GetInt("SelectedGun")] / 100f));
        StartCoroutine(SmoothSpecificationValueChange(AmmoRateImage, AmmoRate[PlayerPrefs.GetInt("SelectedGun")] / 100f));

        // GunsBtns[PlayerPrefs.GetInt("SelectedGun")].Select();
        // GunsBtns[PlayerPrefs.GetInt("SelectedGun")].OnSelect(null);

        //LockImage.SetActive(false);
        //  BuyBtn.SetActive(false);
        SelectBtn.SetActive(true);
        GunPriceText.transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < Guns.Length; i++)
        {
            Guns[i].SetActive(false);
           // HighLighters[i].SetActive(false);
          
            if (PlayerPrefs.GetInt("GunUnlocked"+i) == 1)
            {
             
                LocksImg[i].SetActive(false);
              //  PurchaseBtn.SetActive(false);
            }
            else
            {
               
                LocksImg[i].SetActive(true);
             //   PurchaseBtn.SetActive(true);

            }
        }
        Guns[0].SetActive(true);
        //Guns[PlayerPrefs.GetInt("SelectedGun")].SetActive(true);
       // HighLighters[PlayerPrefs.GetInt("SelectedGun")].SetActive(true);
    }
    void UpdateSelectedGunState()
    {
        for (int i = 0; i < Guns.Length; i++)
        {
          
            if (PlayerPrefs.GetInt("GunUnlocked" + i) == 1)
            {
               
                LocksImg[i].SetActive(false);
            }
            else
            {
               
                LocksImg[i].SetActive(true);
            }
        }
     
    }
    public void GunsBtn(int index)
    {
        MainMenuController.instance.ClickEffect();
        //PlayerPrefs.SetInt("SelectedGun",index);
        PlayerPrefs.SetInt("SelectedGun", index);
        pointer = index;
        TobuyGunInt = index;
       
        for (int i = 0; i < Guns.Length; i++)
        {
            Guns[i].SetActive(false);
          //  GunsPanels[i].SetActive(false);
           // HighLighters[i].SetActive(false);
        }
        Guns[index].SetActive(true);
       // HighLighters[index].SetActive(true);
        GunNameText.text = GunsName[index];

        
        // GunsPanels[index].SetActive(true);
        StartCoroutine(SmoothSpecificationValueChange(RangeSlider, Range[index] / 100f));
        StartCoroutine(SmoothSpecificationValueChange(DamageSlider, Damage[index] / 100f));
        StartCoroutine(SmoothSpecificationValueChange(FireRateSlider, FireRate[index] / 100f));
        StartCoroutine(SmoothSpecificationValueChange(ScopeSlider, Scope[index] / 100f));
        StartCoroutine(SmoothSpecificationValueChange(AmmoRateImage, AmmoRate[index] / 100f));


        if (PlayerPrefs.GetInt("GunUnlocked"+index) == 1)
        {
            //LockImage.SetActive(false);
            BuyBtn.SetActive(false);
            PurchaseBtn.SetActive(false);
            SelectBtn.SetActive(true);
            GunPriceText.transform.parent.gameObject.SetActive(false);
            PlayerPrefs.SetInt("SelectedGun",index);
            PlayerPrefs.SetInt("ShowGun",PlayerPrefs.GetInt("SelectedGun"));
        }
        else
        {
            SelectBtn.SetActive(false);
            PurchaseBtn.SetActive(true);
            //LockImage.SetActive(true);
            BuyBtn.SetActive(true);
            GunPriceText.transform.parent.gameObject.SetActive(true);
            GunPriceText.text = GunsPrice[index].ToString();
        }
        
    }
    IEnumerator SmoothSpecificationValueChange( Image img, float value)
    {
        for (float i = 0; i < value; i+= Time.deltaTime)
        {
            img.fillAmount = i;
            DamageValue.text = ((int)(DamageSlider.fillAmount*100f)).ToString()+"%";
            Firerate.text = ((int)(FireRateSlider.fillAmount * 100f)).ToString() + "%";
            Rangetext.text = ((int)(RangeSlider.fillAmount * 100f)).ToString() + "%";
            Mobility.text = ((int)(ScopeSlider.fillAmount * 100f)).ToString()+"%";
            Ammo.text = ((int)(AmmoRateImage.fillAmount * 100f)).ToString() + "%";
            yield return null;
        }
        img.fillAmount = value;
    }
    public void Buy()
    {

       
    }
    public void BuyNow()
    {
        
        Guns[0].transform.parent.gameObject.SetActive(true);
        MainMenuController.instance.ClickEffect();
        //AreYouSurePanel.SetActive(false);
        //PlayerPrefs.SetInt("GunUnlocked" + pointer, 1);
        if (PlayerPrefs.GetInt("TotalCash") > GunsPrice[pointer])
        {
            PlayerPrefs.SetInt("GunUnlocked" + pointer, 1);
            PlayerPrefs.SetInt("Gun" + TobuyGunInt.ToString() + "Purchased", 1);
        //    PurchaseBtn.SetActive(false);
            PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") - GunsPrice[pointer]);
            MainMenuController.instance.UpdateCashTexts();
            BuyBtn.SetActive(false);
          //  PurchaseBtn.SetActive(false);
            SelectBtn.SetActive(true);

            //LockImage.SetActive(false);

            PlayerPrefs.SetInt("SelectedGun", pointer);

            UpdateSelectedGunState();
          
        }
        else
        {
            StartCoroutine(NotEnoughCoinShow());
            //Purchaser.instance.UnlockAllGuns();
        }
    }

    IEnumerator NotEnoughCoinShow()
    {
        NotEnoughCoins.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        NotEnoughCoins.SetActive(false);
    }
    public void InAppNow()
    {
        
    }
    public void OK()
    {
        Guns[0].transform.parent.gameObject.SetActive(true);
        MainMenuController.instance.ClickEffect();
        
      
    }
    private void OnDisable()
    {
      //  if(Guns[0].transform.parent.gameObject)
          //  Guns[0].transform.parent.gameObject.SetActive(false);
        //for (int i = 0; i < Guns.Length; i++)
        //{
        //    Guns[i].SetActive(false);
        //}
    }
    public void GunUnlockedCongratulations()
    {
        MainMenuController.instance.ClickEffect();
        //LockImage.SetActive(false);
        BuyBtn.SetActive(false);
     //   PurchaseBtn.SetActive(false);
        SelectBtn.SetActive(true);
        GunPriceText.transform.parent.gameObject.SetActive(false);
        PlayerPrefs.SetInt("SelectedGun", pointer);
       
    }
    public void BuyNowVideo()
    {
        Guns[0].transform.parent.gameObject.SetActive(true);
        MainMenuController.instance.ClickEffect();

        PlayerPrefs.SetInt("GunUnlocked" + pointer, 1);
        PlayerPrefs.SetInt("Gun" + TobuyGunInt.ToString() + "Purchased", 1);
        MainMenuController.instance.UpdateCashTexts();
        BuyBtn.SetActive(false);
        //rwdsbtn.SetActive(false);
        SelectBtn.SetActive(true);


        PlayerPrefs.SetInt("SelectedGun", pointer);

        UpdateSelectedGunState();

    }
    public void GunsText(GameObject gun)
    {
        //  GunsForTexture[pointer].GetComponent<Renderer>().material = NewgunMat;
    }
    public void PaintGun(Material NewMat)
    {
        for (int i = 0; i < PainPArts.Length; i++)
        {
            PainPArts[i].GetComponent<MeshRenderer>().materials[0] = NewMat;
        }
    }
}
