using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    public GameObject curntPlayerSpwanPoint, NightSkyboxBtn, DaySkyboxBtn, DirectionalLight;
    
    public GameObject PausePanel, MissionCompPanel, MissionFailPanel, HeadShotObj;
    public Transform Player;

    public Material[] SkyBoxes;
    public Text LevelNoShow;
    public Text FailLevelNoShow;
    public Transform[] ModesPlayerPositions;
    public GameObject[] ModesEnemySpawnObjects;
    public GameObject[] Maps, MapsEffects;
    public int[] LevelEnemies, LevelHeadShotsCount;
    public float[] LevelTime;
    public GameObject TerroristEnemyPrefab, ZombieEnemyPrefab;
    public AudioClip Click;
    public bool CanHeadShot;
    bool levelfailedcheck = false;
    //public Toggle AutoShootToggle;
    
    //public AudioListener MainListner;

    public Text BodyCountsText, HeadShotsCountText,EarnedCoins;
    [Header("Mission Complete Reward Items")]
    //public GameObject[] Stars;
    //public Toggle[] ObjectiveToggles;
    public Text[] RewardTexts;
    public Text TotalReward;

    [Header("Player Arms Materials Data")]
    public Material ArmMaterial;
    public Texture[] ArmsTextures;

    int levelNo;
    int headShotsCount, bodyShotsCount, enemyWaveLength, enemiesSpawned,totalKills,bodyKillScore,HeadKillScore,EarnedCoinsint,getTotal;
    float gameplayTime;
    AudioSource[] thisSources;
    public static GameManager instance;
    public GameObject[] Guns;
    public GameObject[] GunsImage;
    public static int gunsIndex;
    public GameObject[] ModesPlayerPos;
    public static bool HeadShotVisual;
    public static int TotalDeath;
    public static int RequiredDeath;
    public static int countHeadShot, CountKills;
    public Text HeadShotShow, Kills, Total,earnedCoins;
    public Text[] LevelNumber;
    public static int getgunIndex;

    public Text modeTxt;
    public Text lvlTxt;
    public GameObject LoadingPanel;
    public static GameManager Instance;
    // Start is called before the first frame update

    void OnEnable()
    {
        instance = this;
        Maps[PlayerPrefs.GetInt("ModeSelected")].SetActive(true);
        int scr = PlayerPrefs.GetInt("LevelNo") + 1;
        lvlTxt.text =  scr.ToString();
        if (PlayerPrefs.GetInt("ModeSelected") == 0)
        {
            modeTxt.text = "SHIPYARD";
        }
        else if (PlayerPrefs.GetInt("ModeSelected") == 1)
        {
            modeTxt.text = "TERRORIST BASE";
        }
        else if (PlayerPrefs.GetInt("ModeSelected") == 2)
        {
            modeTxt.text = "CONTAINERS";
        }
        else if (PlayerPrefs.GetInt("ModeSelected") == 3)
        {
            modeTxt.text = "BLACK OPS";
        }
        
        

        instance = this;
        for(int i = 0; i < GunsImage.Length; i++) {
            GunsImage[i].SetActive(false);

        }
        if (PlayerPrefs.GetInt("NightSkyBox") == 1)
        {
            RenderSettings.skybox = SkyBoxes[4];
        }
        if (PlayerPrefs.GetInt("DaySkyBox") == 1)
        {
            RenderSettings.skybox = SkyBoxes[0];
        }
        if (PlayerPrefs.GetInt("NightSkyBox") != 1 || PlayerPrefs.GetInt("DaySkyBox") != 1)
        {
            RenderSettings.skybox = SkyBoxes[PlayerPrefs.GetInt("ModeSelected")];
        }
        thisSources = gameObject.GetComponents<AudioSource>();
        for(int i = 0; i < LevelNumber.Length; i++)
        {
            LevelNumber[i].text = (PlayerPrefs.GetInt("LevelNo")+1).ToString();
        }
        //levelNo = PlayerPrefs.GetInt("LevelNo");
      //  FailLevelNoShow.text = (levelNo + 1).ToString();
     //   LevelNoShow.text = (levelNo + 1).ToString();
        Debug.Log("Level No PLayer Pref:" + PlayerPrefs.GetInt("LevelNo"));
       
       // ModesPlayerPositions[PlayerPrefs.GetInt("ModeSelected")].childCount
        int rand = Random.Range(0,5);

        ModesEnemySpawnObjects[PlayerPrefs.GetInt("ModeSelected")].SetActive(true);
        //Maps[PlayerPrefs.GetInt("ModeSelected")].SetActive(true);

        if(PlayerPrefs.GetInt("ModeSelected") == 3)
        {
            Player.transform.position = curntPlayerSpwanPoint.transform.position;
            Player.transform.rotation = curntPlayerSpwanPoint.transform.rotation;
        }
        else
        {
            Player.transform.position = ModesPlayerPositions[PlayerPrefs.GetInt("ModeSelected")].GetChild(rand).transform.position;
            Player.transform.rotation = ModesPlayerPositions[PlayerPrefs.GetInt("ModeSelected")].GetChild(rand).transform.rotation;
            Debug.Log("ModeSelected from ModeSelection : " + PlayerPrefs.GetInt("ModeSelected"));
        }
        
           
        
        Debug.Log("Selected Mode : "+PlayerPrefs.GetInt("ModeSelected"));
        Debug.Log("Map Selected:"+ Maps[PlayerPrefs.GetInt("ModeSelected")].name);

        

        Player.gameObject.SetActive(true);

        if(PlayerPrefs.GetInt("Music",1) == 1)
        {
            thisSources[0].Play();
        }

        

    }
    void Start()
    {
       // PlayerPrefs.DeleteAll();
    // PlayerPrefs.SetInt("LevelNo",14);
       Debug.Log("Level PlayerPref "+PlayerPrefs.GetInt("LevelNo"));
        Debug.Log("Gun Name"+ GunsImage[PlayerPrefs.GetInt("Gun" + GunSelectionController.TobuyGunInt.ToString() + "Purchased")].name);
        PlayerPrefs.SetInt("AutoShoot", 0);

        //levelNo = 2;

        Time.timeScale = 1;
        // length count logic
        if (LevelEnemies[levelNo] < 8)
        {
            enemyWaveLength = 3;
        }
        else if(LevelEnemies[levelNo] >=8)
        {
            enemyWaveLength = 4;
        }
        else if(LevelEnemies[levelNo] >= 13)
        {
            enemyWaveLength = 5;
        }

        Invoke("SpawnEnemies", 2f);
        levelNo = PlayerPrefs.GetInt("LevelNo");
        HeadShotsCountText.text = string.Format("{00:00}/{01:00}", headShotsCount, LevelHeadShotsCount[levelNo]);
        BodyCountsText.text = string.Format("{00:00}/{01:00}", bodyShotsCount, LevelEnemies[levelNo]);
        
        TimerController.instance.StartTimer(LevelTime[levelNo]);

        //ChechShootToggle();

        ArmMaterial.mainTexture = ArmsTextures[Random.Range(0, ArmsTextures.Length)];
        TotalDeath = LevelHeadShotsCount[levelNo] + LevelEnemies[levelNo];
      //  Debug.LogError("Total Requird Death:"+ TotalDeath);
        TotalDeath -= 1;
        RequiredDeath = bodyShotsCount + headShotsCount;
        Debug.Log("Still Killed"+ RequiredDeath);
        Analytics.CustomEvent("m" + PlayerPrefs.GetInt("ModeSelected") + "_LevelStart", new Dictionary<string, object>
        {
            { "gameplay", levelNo}
        });

        setStartAnalytics();
    }
    public void setStartAnalytics()
    {
    //    Firebase.Analytics.FirebaseAnalytics.SetUserProperty("Mode" + PlayerPrefs.GetInt("ModeSelected") + "LevelStart", levelNo.ToString());
    }
    public void setFailedAnalytics()
    {
    //    Firebase.Analytics.FirebaseAnalytics.SetUserProperty("Mode" + PlayerPrefs.GetInt("ModeSelected") + "LevelFailed", levelNo.ToString());
    }
    public void setCompleteAnalytics()
    {
    //    Firebase.Analytics.FirebaseAnalytics.SetUserProperty("Mode" + PlayerPrefs.GetInt("ModeSelected") + "LevelComplete", levelNo.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        gameplayTime += Time.deltaTime;
       
        //    Debug.Log("TotalDeath Death Required"+ TotalDeath);
    }
    //void ChechShootToggle()
    //{
    //    if (PlayerPrefs.GetInt("AutoShoot", 1) == 1)
    //    {
    //        AutoShootToggle.isOn = true;
    //    }
    //    else
    //    {
    //        AutoShootToggle.isOn = false;
    //    }
    //}
    //public void ShootToggle(bool toggle)
    //{
    //    if (toggle)
    //    {
    //        PlayerPrefs.SetInt("AutoShoot", 1);

    //    }
    //    else
    //    {
    //        PlayerPrefs.SetInt("AutoShoot", 0);
    //    }
    //}
    public void SpawnEnemies()
    {

        Vector3[] tempArray = SpawnPointsGetter.instance.GetPoints(enemyWaveLength, 19.5f, 60f);
        
       if(PlayerPrefs.GetInt("ModeSelected")==0)
        {
            for (int i = 0; i < enemyWaveLength; i++)
            {
                if (enemiesSpawned < LevelEnemies[levelNo])
                {
                    Instantiate(TerroristEnemyPrefab, tempArray[i], ZombieEnemyPrefab.transform.rotation);
                    enemiesSpawned++;
                }
            }
        }
        else
        {
            for (int i = 0; i < enemyWaveLength; i++)
            {
                if (enemiesSpawned < LevelEnemies[levelNo])
                {
                    Instantiate(ZombieEnemyPrefab, tempArray[i], TerroristEnemyPrefab.transform.rotation);
                    enemiesSpawned++;
                }
            }
       }
        
    }
    public void BtnAction(string name)
    {
        ClickEffect();
        if (name == "restart")
        {
            levelfailedcheck = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (name == "clearlevelrestart")
        {
            PlayerPrefs.SetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (name == "pause")
        {

            Time.timeScale = 0;
            PausePanel.SetActive(true);
            
        }
        else if (name == "resume")
        {
            Time.timeScale = 1;
            levelfailedcheck = false;
            PausePanel.SetActive(false);

        }
        else if (name == "home")
        {
            levelfailedcheck = false;
            Time.timeScale = 1f;

           
            SceneManager.LoadScene("MainMenu");
        }
        else if (name == "next")
        {
            levelfailedcheck = false;
            PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") + getTotal);

            if (LevelNoShow.text == "30") {
                Debug.Log("LoadMainMenu");
                SceneManager.LoadSceneAsync(1);
            }
            Time.timeScale = 1f;
            if ((levelNo) == PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")))
            {
                PlayerPrefs.SetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")) + 1);
                PlayerPrefs.SetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), 0) + 1);
                //   PlayerPrefs.SetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), 0) + 1);
            }
            PlayerPrefs.SetInt("LevelNo", ++levelNo);
            if (levelNo == LevelEnemies.Length)
            {
                levelNo = 0;
            }

            
            MainMenuController.fromGameplay = true;
            LoadingPanel.SetActive(true);
            FindObjectOfType<LoadingLevelController>().LoadLevel("GamePlay");
        //    SceneManager.LoadScene("Gameplay");
        } 
        
        else if (name== "homenext") {
            PlayerPrefs.SetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")) + 1);
            PlayerPrefs.SetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), 0) + 1);
            SceneManager.LoadScene("MainMenu");
        } else if (name == "homeClearPanel")
        {
            PlayerPrefs.SetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")) + 1);
            PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") + getTotal);
            SceneManager.LoadScene("MainMenu");
            PlayerPrefs.SetInt("FromGamePlayHome",1);

            print("Curnt level Gameplay...... : " + PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")));
        }
        else if (name == "rateus")
        {
            PlayerPrefs.SetInt("Rated", 1);
            //     RateUsPanel.SetActive(false);
        }
        else if (name == "later")
        {
            //    RateUsPanel.SetActive(false);
        }
    }

   
    public void LevelComplete()
    {

        // FailLevelNoShow.text = (levelNo + 1).ToString();
        HeadShotShow.text = headShotsCount.ToString();
        Kills.text = bodyShotsCount.ToString();
        totalKills = headShotsCount + bodyShotsCount;
        Total.text = totalKills.ToString();
        bodyKillScore = bodyShotsCount * 100;
        HeadKillScore = headShotsCount * 200;
        getTotal = bodyKillScore + HeadKillScore;
        EarnedCoins.text = getTotal.ToString();
         playercontroller.instance.respondstoInput = false;
        StartCoroutine(LevelCompleteWait());
    }
    IEnumerator LevelCompleteWait()
    {
        LevelNoShow.text = levelNo.ToString();

     
    Debug.Log("Reward is:"+PlayerPrefs.GetInt("TotalCash"));
        Total.text = totalKills.ToString();
        yield return new WaitForSeconds(2.5f);
        

      
        Time.timeScale = 0;
        MissionCompPanel.SetActive(true);

        // setCompleteAnalytics();
        Analytics.CustomEvent("m" + PlayerPrefs.GetInt("ModeSelected") + "_LevelComplete", new Dictionary<string, object>
            {
                { "gameplay", levelNo}
            });

        float tempValue = 0;
        int totalRewardTemp = 0, starCount = 1;
        bool[] starsFlag = new bool[3];
        int[] rewardsTemp = { 500, 100, 100 };

        starsFlag[0] = true;
        if (headShotsCount >= LevelHeadShotsCount[levelNo])
        {
            starsFlag[1] = true;
            starCount++;
        }
        if (gameplayTime <= LevelTime[levelNo])
        {
            starsFlag[2] = true;
            starCount++;
        }
        // Level Unlock System
        Debug.Log("Previous Player Prefs: "+PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")));
        Debug.Log("Level 1 Increment: "+levelNo);
       /* if ((levelNo) == PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")))
        {
            PlayerPrefs.SetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected")) + 1);
            //PlayerPrefs.SetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"),PlayerPrefs.GetInt("LevelsUnlocked" + PlayerPrefs.GetInt("ModeSelected"), 1) + 1);
        }*/
        yield return new WaitForSeconds(3f);

       

        for (int i = 0; i < 3; i++)
        {
            if (starsFlag[i])
            {
                totalRewardTemp += rewardsTemp[i];
                tempValue = 0;
             //   Stars[i].SetActive(true);
              //  ObjectiveToggles[i].isOn = true;

                for (float j = 0; j <= 1; j += Time.deltaTime)
                {
                    tempValue = Mathf.Lerp(0, rewardsTemp[i], j);
                    RewardTexts[i].text = ((int)tempValue).ToString();
                    yield return null;
                }
                RewardTexts[i].text = rewardsTemp[i].ToString();

            }
            else
            {
                RewardTexts[i].color = Color.red;
                RewardTexts[i].text = "X";
            }
        }

        //TotalReward.GetComponent<Animator>().enabled = true;
        for (float j = 0; j <= 1; j += Time.deltaTime)
        {
            //TotalReward.GetComponent<Animator>().enabled = true;
            tempValue = Mathf.Lerp(0, totalRewardTemp, j);
            TotalReward.text = ((int)tempValue).ToString();
            yield return null;
        }
        TotalReward.text = totalRewardTemp.ToString();

        //PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") + totalRewardTemp);
    }
    public void LevelFail()
    {

        if (levelfailedcheck == false)
        {

            levelfailedcheck = true;
        }
           
        //  FailLevelNoShow.text = (levelNo + 1).ToString();
        //isAim = false;
        StartCoroutine(LevelFailWait());
    }
    IEnumerator LevelFailWait()
    {
        Time.timeScale = 0;
        MissionFailPanel.SetActive(true);

       
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            //thisSources[0].clip = MissionCompFailBGClip;
            thisSources[0].Play();
        }

       // setFailedAnalytics();
        Analytics.CustomEvent("m" + PlayerPrefs.GetInt("ModeSelected") + "_LevelFail", new Dictionary<string, object>
        {
            { "gameplay", levelNo}
        }
        );

        yield return new WaitForSeconds(0.5f);
        
    }
    public void DoubleReward()
    {

        Debug.Log("Get Total Before Double" + getTotal);

        PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash") + getTotal * 2);
        Debug.Log("Get Total After Double" + getTotal);
        Debug.Log("Coins after Double" + PlayerPrefs.GetInt("TotalCash"));
    }
    public void CountDead(bool isHeadShot)
    {
        if (isHeadShot)
        {
            headShotsCount++;
            HeadShotsCountText.text = string.Format("{00:00}/{01:00}", headShotsCount, LevelHeadShotsCount[levelNo]);
            if(headShotsCount >= LevelHeadShotsCount[levelNo])
            {
                HeadShotsCountText.color = Color.green;
                /*
                 call any effect here for headshots completed 
                 */

            }
            StartCoroutine(HeadShotWait());
        }
        
        bodyShotsCount++;
        BodyCountsText.text = string.Format("{00:00}/{01:00}", bodyShotsCount, LevelEnemies[levelNo]);

        
        if (bodyShotsCount >= LevelEnemies[levelNo])
        {
            BodyCountsText.color = Color.green;
            LevelComplete();
        }
        else if (enemiesSpawned == bodyShotsCount)
        {
            Invoke("SpawnEnemies", 2f);
        }
    }
    IEnumerator HeadShotWait()
    {
        HeadShotObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        HeadShotObj.SetActive(false);

    }
    public void ChangeSkyboxNight()
    {
        PlayerPrefs.SetInt("NightSkyBox", 1);
        RenderSettings.skybox = SkyBoxes[4];
        NightSkyboxBtn.SetActive(false);
        DirectionalLight.SetActive(false);
        DaySkyboxBtn.SetActive(true);
    }
    public void ChangeSkyboxDay()
    {
        PlayerPrefs.SetInt("DaySkyBox", 1);
        RenderSettings.skybox = SkyBoxes[0];
        NightSkyboxBtn.SetActive(true);
        DirectionalLight.SetActive(true);
        DaySkyboxBtn.SetActive(false);
    }
    public void ClickEffect()
    {
        if (Click != null && thisSources[1] != null && PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            thisSources[1].PlayOneShot(Click);
        }
    }
    public void GunsChange()
    {
        if (gunsIndex < Guns.Length-1)
        {
           // Debug.Log("Gun Index IS:" + gunsIndex);
            gunsIndex++;
           
            getgunIndex = gunsIndex;
            Debug.Log("Gun Index is : "+ gunsIndex);
            for (int i = 0; i < Guns.Length; i++)
            {
                Guns[i].SetActive(false);
                GunsImage[i].SetActive(false);
            }
            Guns[gunsIndex].SetActive(true);
            GunsImage[gunsIndex].SetActive(true);
        }else
        {
            for (int i = 0; i < Guns.Length; i++)
            {
                Guns[i].SetActive(false);
                GunsImage[gunsIndex].SetActive(false);
            }
            gunsIndex=0;
            Guns[0].SetActive(true);
            GunsImage[0].SetActive(true);
        }
    }

    public void SetTimeScale()
    {
        Time.timeScale = 1;
    }

}
