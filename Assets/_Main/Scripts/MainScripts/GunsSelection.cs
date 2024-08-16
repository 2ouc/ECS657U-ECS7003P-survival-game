using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using WSMGameStudio.RailroadSystem;

public class GunsSelection : MonoBehaviour
{
    [Header("Guns Models")]
    [SerializeField]
    private GunsData[] gunData;
    [Header("Guns Name")]
    [SerializeField]
    private Text GunName;
    [Header("Spawns Point for the Guns")]
    [SerializeField]
    private Transform[] GunsSpawnPoint;
    [Header("Guns Button")]
    [SerializeField]
    private Button[] GunButtons;
    [Header("Price of the Guns")]
    [SerializeField]
    private Text PriceText;
    [SerializeField]
    private Text GunsPrice;
    [Header("Buy Button for purchasinng Guns")]
    [SerializeField]
    private GameObject BuyButton;
    [Header("Select Button for Guns")]
    [SerializeField]
    private GameObject SelectButton;
    [Header("Gun Specifications")]
    [SerializeField]
    private Image DamageImage;
    [SerializeField]
    private Image FireRateImage;
    [SerializeField]
    private Image MobilityImage;
    [SerializeField]
    private Image AmmoImage;
    [SerializeField]
    private Image RangeImage;
    [Header("Guns Percentage Text Value")]
    [SerializeField]
    private Text DamageValueText;
    [SerializeField]
    private Text FireRateText;
    [SerializeField]
    private Text MobilityText;
    [SerializeField]
    private Text AmmoText;
    [SerializeField]
    private Text RangeText;
    [Header("Extra Panels")]
    [SerializeField]
    private GameObject MainMenu;
    [SerializeField]
    private GameObject ModeSelection;

    public static int currentGun;

    GameObject GunsSpawnModel;

   
    private void OnEnable()
    {
      
        ShowGunsFromList();
    }


    public void GunsInstantiated()
    {
        GunsSpawnModel = Instantiate(gunData[currentGun].SelectionModel, GunsSpawnPoint[currentGun].position, GunsSpawnPoint[currentGun].rotation);
        GunsSpawnModel.transform.SetParent(GunsSpawnPoint[currentGun]);

        for (int i = 0; i < GunButtons.Length; i++)
        {
            if (gunData[i].locked == true)
            {

                GunButtons[i].gameObject.transform.Find("Lock").gameObject.SetActive(true);
            }
            else
            {

                GunButtons[i].gameObject.transform.Find("Lock").gameObject.SetActive(false);
            } 
        }
        StartCoroutine(SmoothSpecificationValueChange(DamageImage, gunData[currentGun].Damage / 100f));
        StartCoroutine(SmoothSpecificationValueChange(FireRateImage, gunData[currentGun].FireRate / 100f));
        StartCoroutine(SmoothSpecificationValueChange(MobilityImage, gunData[currentGun].Mobility / 100f));
        StartCoroutine(SmoothSpecificationValueChange(RangeImage, gunData[currentGun].Range / 100f));
        StartCoroutine(SmoothSpecificationValueChange(AmmoImage, gunData[currentGun].Ammo / 100f));

    }

    void ShowGunsFromList()
    {
        GunsInstantiated();

        if (gunData[currentGun].locked==true)
        {

            SelectButton.SetActive(false);
            BuyButton.SetActive(true);
            GunsPrice.gameObject.SetActive(true);
            PriceText.gameObject.SetActive(true);
            GunName.text= gunData[currentGun].Name.ToString();
            GunsPrice.text = gunData[currentGun].Price.ToString();
            DamageImage.fillAmount = gunData[currentGun].Damage;
            FireRateImage.fillAmount = gunData[currentGun].FireRate;
            MobilityImage.fillAmount = gunData[currentGun].Mobility;
            AmmoImage.fillAmount = gunData[currentGun].Ammo;
            RangeImage.fillAmount = gunData[currentGun].Range;
           

        }
        else
        {
            
            SelectButton.SetActive(true);
            BuyButton.SetActive(false);
            GunName.text = gunData[currentGun].Name.ToString();
            GunsPrice.gameObject.SetActive(false);
            PriceText.gameObject.SetActive(false);
        }

    }


    IEnumerator SmoothSpecificationValueChange(Image img, float value)
    {
        for (float i = 0; i < value; i += Time.deltaTime)
        {
            img.fillAmount = i;
            // Debug.Log("ImageFillamountis"+img.fillAmount);
            DamageValueText.text = ((int)(DamageImage.fillAmount * 100f)).ToString() + "%";
            FireRateText.text = ((int)(FireRateImage.fillAmount * 100f)).ToString() + "%";
            RangeText.text = ((int)(RangeImage.fillAmount * 100f)).ToString() + "%";
            MobilityText.text = ((int)(MobilityImage.fillAmount * 100f)).ToString() + "%";
            AmmoText.text = ((int)(AmmoImage.fillAmount * 100f)).ToString() + "%";
            yield return null;
        }
        img.fillAmount = value;
       

    }

    #region ifthereisArrowButton
    public void onClickNext()
    {

        Destroy(GunsSpawnModel);
        FindObjectOfType<MainMenuSound>().BtnSoundPlay();


        if (currentGun < gunData.Length - 1)
        {
            currentGun = currentGun + 1;
        }
        else
        {
            currentGun = 0;
        }

        ShowGunsFromList();

    }
    public void onClickPrev()
    {
        Destroy(GunsSpawnModel);
        FindObjectOfType<MainMenuSound>().BtnSoundPlay();

        if (currentGun == 0)
        {
            currentGun = gunData.Length - 1;
        }
        else
        {
            currentGun = currentGun - 1;
        }
        ShowGunsFromList();
    }
    #endregion


    public void BuyGuns()
    {
       // FindObjectOfType<MainMenuSound>().BtnSoundPlay();
        if (gunData[currentGun].Price <= PlayerPrefs.GetInt("Coins"))
        {
            gunData[currentGun].IsUnlocked = true;
            gunData[currentGun].locked = false;

            PlayerPrefs.SetInt("Coins" , PlayerPrefs.GetInt("Coins") - gunData[currentGun].Price);
            FindObjectOfType<CoinsManager>().UpdateUI();

          
            SelectButton.SetActive(true);
            BuyButton.SetActive(false);
            GunsPrice.gameObject.SetActive(false);
            PriceText.gameObject.SetActive(false);

            

        }
        else
        {
                            //Purchaser.instance.Buy_Trains();
            ShowGunsFromList();
           
        }
    }

    
    public void SelectGuns(int num)
    {
       // FindObjectOfType<MainMenuSound>().BtnSoundPlay();

        for (int i = 0; i < GunButtons.Length; i++)
        {
            //if (i == num)
            //{
               
            //    GunButtons[num].gameObject.transform.Find("Glow").gameObject.SetActive(true);
            //}
            //else
            //{
              
            //    GunButtons[i].gameObject.transform.Find("Glow").gameObject.SetActive(false);
            //}

            
                Destroy(GunsSpawnModel);
                currentGun = num;
                ShowGunsFromList();
           
        }

        
       
    }



    public void NextButton()
    {

        for (int i = 0; i < gunData.Length; i++)
        {
            if (gunData[currentGun].IsUnlocked == true)
            {
                gunData[currentGun].Selected = true;
                
            }
            else
            {
                gunData[i].Selected = false;
               
            }

        }
        Destroy(GunsSpawnModel);
        ModeSelection.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Back()
    {
        Destroy(GunsSpawnModel);
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
   public void BtnSound()
    {
        FindObjectOfType<MainMenuSound>().BtnSoundPlay();
    }
   
}
