using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenuPanel, GunsSelectionPanel, ModeSelectionPanel, LevelSelectionPanel,
        SettingsPanel, StorePanel, ExitPanel, DailyRewardPanel, Character,guns,inappStorePanel;
    //public GameObject[] MainMenuPlayers;
    public GameObject[] MusicToggle, SoundToggle;
    public string MoreGamesURL, RateUsURL, PrivacyPolicyURL;
    public AudioClip Click;
    public Text[] TotalCashTexts;

    AudioSource[] thisSources;
    public static bool fromGameplay;
    public static MainMenuController instance;
    public GameObject[] SettingTopButtons;
    private string ControlsHandle;
    public GameObject[] SettingPanels;
    public GameObject MoreGames;
    //date time variables for daily reward
    
    DateTime currentDate;
    DateTime oldDate;

    public GameObject unlockeverything, unlockAllGuns, UnlockAllModes;

    //end
    
    private void Start()
    {
        //PlayerPrefs.SetInt("TotalCash", 50000);
        UpdateCashTexts();
        Debug.Log("Menu Start"+PlayerPrefs.GetInt("LevelsUnlocked") + PlayerPrefs.GetInt("ModeSelected"));
        if (PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")) >= 16)
        {
            MainMenuPanel.SetActive(true);
            ModeSelectionPanel.SetActive(false);
            LevelSelectionPanel.SetActive(false);
           
            fromGameplay = false;
        }
        if ((PlayerPrefs.GetInt("FromGamePlayHome") == 1) && (PlayerPrefs.GetInt("ModeSelected")) <= 16)
        {
            MainMenuPanel.SetActive(true);
            ModeSelectionPanel.SetActive(false);
            LevelSelectionPanel.SetActive(false);
        }
    }
    // Start is called before the first frame update
   
    void OnEnable()
    {
        if(PlayerPrefs.GetInt("LevelsUnlocked" + 3) >= 29)
        {
            UnlockAllModes.SetActive(false);
        }

        if(PlayerPrefs.GetInt("GunUnlocked" + 9) == 1 && PlayerPrefs.GetInt("LevelsUnlocked" + 3) >= 29 && PlayerPrefs.GetInt("ADSUNLOCK") == 1)
        {
            unlockeverything.SetActive(false);
        }

        if (PlayerPrefs.GetInt("GunUnlocked" + 9) == 1)
        {
            unlockAllGuns.SetActive(false);
        }

        instance = this;

        //PlayerPrefs.SetInt("LevelsUnlocked" + 0, 15);
        //PlayerPrefs.SetInt("LevelsUnlocked" + 1, 15);
        //PlayerPrefs.SetInt("LevelsUnlocked" + 2, 15);
        //PlayerPrefs.SetInt("LevelsUnlocked" + 3, 15);

        //PlayerPrefs.SetInt("TotalCash",10000000);
       
        
        thisSources = gameObject.GetComponents<AudioSource>();

            UpdateCashTexts();
            CheckToggleSound();
            CheckToggleMusic();

            //Store the current time when it starts
            currentDate = System.DateTime.Now;

            //Grab the old time from the player prefs as a long
            long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString", currentDate.ToBinary().ToString()));

            //Convert the old time from binary to a DataTime variable
            oldDate = DateTime.FromBinary(temp);

            //Use the Subtract method and store the result as a timespan variable
            TimeSpan difference = currentDate.Subtract(oldDate);

            //if (difference.TotalHours > 23 || !PlayerPrefs.HasKey("sysString"))
            //{
            //    DailyRewardPanel.SetActive(true);
            //    MainMenuPanel.SetActive(false);
            //}
            if (fromGameplay)
            {
                MainMenuPanel.SetActive(false);

                LevelSelectionPanel.SetActive(false);
                if (PlayerPrefs.GetInt("ShowModePanel") == 1)
                {
                    ModeSelectionPanel.SetActive(true);
                }
                else
                {
                    LevelSelectionPanel.SetActive(true);
                }
                fromGameplay = false;
            }
        
    }
        // Update is called once per frame
        void Update()
        {

        }

        public void MainMenuBtnAction(string name)
        {
            ClickEffect();
            if (name.Equals("start"))
            {
                
                ModeSelectionPanel.SetActive(true);
                MainMenuPanel.SetActive(false);
                Character.SetActive(true);
                if (PlayerPrefs.GetInt("ADSUNLOCK") == 0)
                {
                  /*  if (McFairyAdsMediation.Instance)
                    {
                        McFairyAdsMediation.Instance.ShowInterstitialAd(0);
                    }*/
                }
            }

            else if (name.Equals("Settings"))
            {
                SettingsPanel.SetActive(true);
                MainMenuPanel.SetActive(false);
            }
            else if (name.Equals("exit"))
            {
                MainMenuPanel.SetActive(false);
                ExitPanel.SetActive(true);
            }
            else if (name.Equals("store"))
            {
                MainMenuPanel.SetActive(false);
                StorePanel.SetActive(true);
            }
            else if (name.Equals("more"))
            {
                Application.OpenURL("https://play.google.com/store/apps/dev?id=6570343943680071013");
                
            } else if (name.Equals("closeMore"))
            {
                MoreGames.SetActive(false);
            }else if (name.Equals("noAds"))
        {
            MoreGames.SetActive(true);
        }
            else if (name.Equals("openURL"))
            {
                Application.OpenURL("https://play.google.com/store/apps/dev?id=6570343943680071013");
            } else if (name.Equals("CloseInapp"))
            {
                MoreGames.SetActive(false);
            }
            else if (name.Equals("rate"))
            {
                Application.OpenURL(RateUsURL);
            }
            else if (name.Equals("policy"))
            {
                Application.OpenURL("https://sites.google.com/view/gaming-storm/privacy-policy");
            }
            else if (name.Equals("InappOffer"))
            {
                StorePanel.SetActive(true);
        }
        else if (name.Equals("inappstorepanel"))
        {
            inappStorePanel.SetActive(true);
            MainMenuPanel.SetActive(false);
        }
            else if (name.Equals("InappOfferClose"))
            {
                StorePanel.SetActive(false);
            MainMenuPanel.SetActive(true);
        }
        else if(name.Equals("StorePanelClose"))
        {
            inappStorePanel.SetActive(false);
            MainMenuPanel.SetActive(true);
        }
            else if (name.Equals("BuyCoins"))
        {
           // Debug.Log("Inapp Panel Purchase Coins");
        }else if (name.Equals("ModeBack"))
        {
            Character.SetActive(true);
        }
        }
        public void ExitBtnAction(string name)
        {
            ClickEffect();
            if (name.Equals("yes"))
            {
                Application.Quit();
            }
            else if (name.Equals("no"))
            {
                ExitPanel.SetActive(false);
                MainMenuPanel.SetActive(true);
            }
        }
        public void BackBtnAction()
        {
            ClickEffect();
            if (MainMenuPanel.activeInHierarchy)
            {
                MainMenuPanel.SetActive(false);
                ExitPanel.SetActive(true);
  
        }

            if (ModeSelectionPanel.activeInHierarchy)
            {
                ModeSelectionPanel.SetActive(false);
            Character.SetActive(true);
            MainMenuPanel.SetActive(true);
                
        }
            if (GunsSelectionPanel.activeInHierarchy)
            {
                GunsSelectionPanel.SetActive(false);
            guns.SetActive(false);
                ModeSelectionPanel.SetActive(true);
                Character.SetActive(true);
                
            }
            if (LevelSelectionPanel.activeInHierarchy)
            {
                Character.SetActive(false);
            ModeSelectionPanel.SetActive(true);
                LevelSelectionPanel.SetActive(false);
            }
            if (SettingsPanel.activeInHierarchy)
            {
                SettingsPanel.SetActive(false);
                MainMenuPanel.SetActive(true);
            }
            if (LevelSelectionPanel.activeInHierarchy)
            {

                LevelSelectionPanel.SetActive(false);
                ModeSelectionPanel.SetActive(true);

            }
            if (StorePanel.activeInHierarchy)
            {
                StorePanel.SetActive(false);
                MainMenuPanel.SetActive(true);
            }
            if (SettingsPanel.activeInHierarchy)
            {
                SettingsPanel.SetActive(false);
                MainMenuPanel.SetActive(true);
            }


        }
        public void StorePaneltnAction(string name)
        {
            ClickEffect();

        }
        public void GunSelectionBtnAction(string name)
        {
       
        ClickEffect();
            Character.SetActive(true);
            if (name == "start")
            {
                GunsSelectionPanel.SetActive(false);
                ModeSelectionPanel.SetActive(true);
            guns.SetActive(false);
            }
        }
        public void ModeSelectionBtnAction(string name)
        {
            ClickEffect();
            if (name.Equals("gun"))
            {
                ModeSelectionPanel.SetActive(false);
                LevelSelectionPanel.SetActive(true);
            }
        }
        void CheckToggleSound()
        {
            if (PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                SoundToggle[0].SetActive(false);
                SoundToggle[1].SetActive(true);
            }
            else
            {
                SoundToggle[0].SetActive(true);
                SoundToggle[1].SetActive(false);
            }
        }
        void CheckToggleMusic()
        {
            if (PlayerPrefs.GetInt("Music", 1) == 1)
            {
                MusicToggle[0].SetActive(false);
                MusicToggle[1].SetActive(true);
                thisSources[0].Play();
            }
            else
            {
                MusicToggle[0].SetActive(true);
                MusicToggle[1].SetActive(false);
                thisSources[0].Stop();
            }
        }
        public void SoundToggleFunction()
        {
            if (PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                PlayerPrefs.SetInt("Sound", 0);
            }
            else
            {
                PlayerPrefs.SetInt("Sound", 1);
            }
            CheckToggleSound();
            ClickEffect();
            Debug.Log(PlayerPrefs.GetInt("Sound") + "");
        }
        public void MusicToggleFunction()
        {
            if (PlayerPrefs.GetInt("Music", 1) == 1)
            {
                PlayerPrefs.SetInt("Music", 0);
            }
            else
            {
                PlayerPrefs.SetInt("Music", 1);
            }
            CheckToggleMusic();
            ClickEffect();

        }
        public void ClickEffect()
        {
            //if (Click != null && thisSources[1] != null && PlayerPrefs.GetInt("Sound", 1) == 1)
            //{
            //    thisSources[1].PlayOneShot(Click);
            //}
        }
        public void UpdateCashTexts()
        {
            for (int i = 0; i < TotalCashTexts.Length; i++)
            {
                TotalCashTexts[i].text = PlayerPrefs.GetInt("TotalCash").ToString();

            }
        }
        public void ActiveArea(GameObject Activebutton)
        {
            for (int i = 0; i < SettingTopButtons.Length; i++)
            {
                SettingTopButtons[i].SetActive(false);

            }

            Activebutton.SetActive(true);

        }
        public void ActivePAnels(GameObject ActivePanels)
        {
            for (int i = 0; i <= 2; i++)
            {
                Debug.Log("i value: " + i);
                //SettingTopButtons[i].SetActive(false);
                SettingPanels[i].SetActive(false);

            }

            ActivePanels.SetActive(true);

        }

    public void Show_Interstitial()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK") == 0)
        {
            
        }
    }

    public void UnlockEverything()
    {
        if (PlayerPrefs.GetInt("GunUnlocked" + 9) != 1 && PlayerPrefs.GetInt("LevelsUnlocked" + 3) != 29 && PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            unlockeverything.SetActive(false);
        }
    }

    public void UnlockGuns()
    {
        if (PlayerPrefs.GetInt("GunUnlocked" + 9) != 1)
        {
            unlockAllGuns.SetActive(false);
        }
    }

    public void UnlockModes()
    {
        if (PlayerPrefs.GetInt("LevelsUnlocked" + 3) != 29)
        {
            UnlockAllModes.SetActive(false);
        }
    }
} 

