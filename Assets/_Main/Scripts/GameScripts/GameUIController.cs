using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using WSMGameStudio.RailroadSystem;
public class GameUIController : MonoBehaviour
{
    /*

    [Header("Train for Controlling")]
    [SerializeField]
    private TrainController_v3 train;
    [Header("Waggon for Controlling")]
    [SerializeField]
    private Wagon_v3[] waggon;
    [Header("Text For Train Speed")]
    [SerializeField]
    private Text TrainSpeedText;
    [Header("Sliders for Controlling Train")]
    [SerializeField]
    private Slider TrainSpeedSlider;
    [SerializeField]
    private Slider TrainBrakeSlider;
    [SerializeField]
    public bool TrainBrakeCheck;
    [Header("Level Complete Panel")]
    [SerializeField]
    public GameObject levelComplete;
    [Header("Level Failed Panel")]
    [SerializeField]
    public GameObject levelFailed;

    public TimerDisplay timerDisplay;

    bool tranBrake = false;
    bool waggonCheck=true;

    Transform TrainInteror;
    bool checkinterior=false;


    Transform TrainInteriorLights;
    Transform TrainExteriorLights;
    private void Awake()
    {
        Time.timeScale = 1;
    }


    private void Start()
    {
        // Invoke for getting Train Component
        // because Train is Instantiated
       // Invoke("TrainComponent", 2f);
    }

    #region TrainComponent
    //void TrainComponent()
    //{
    //    if (train == null)
    //    {
    //        train = GameObject.FindGameObjectWithTag("Player").GetComponent<TrainController_v3>();
            
    //        timerDisplay = GameObject.FindObjectOfType<TimerDisplay>();
            
    //    }
    //    else
    //    {

    //        return;
    //    }
    //}
    #endregion

    #region TrainFunctionallity


    public void TrainFinding()
    {
        train = GameObject.FindGameObjectWithTag("Player").GetComponent<TrainController_v3>();
        //Waggon Finder
        GameObject[] wagon = GameObject.FindGameObjectsWithTag("Waggon");
        waggon = new Wagon_v3[waggon.Length];
        for (int i = 0; i < waggon.Length; ++i)
        {
            waggon[i] = wagon[i].GetComponent<Wagon_v3>();

        }
    }


    private void Update()
    {
        while (train == null)
        {
            train = GameObject.FindGameObjectWithTag("Player").GetComponent<TrainController_v3>();
            //Waggon Finder
            GameObject[] wagon = GameObject.FindGameObjectsWithTag("Waggon");
            waggon = new Wagon_v3[waggon.Length];
            for (int i = 0; i < waggon.Length; ++i)
            {
                waggon[i] = wagon[i].GetComponent<Wagon_v3>();

            }

            TrainInteriorLights = train.transform.Find("Lights").gameObject.transform.Find("TrainNewLights").gameObject.transform.Find("Interiorlight");
            TrainExteriorLights = train.transform.Find("Lights").gameObject.transform.Find("TrainNewLights").gameObject.transform.Find("ExteriorLight");

            if (PlayerPrefs.GetInt("Mode_no") == 4)
            {
                TrainInteriorLights.gameObject.SetActive(true);
                TrainExteriorLights.gameObject.SetActive(true);

            }
            else
            {
                TrainInteriorLights.gameObject.SetActive(false);
                TrainExteriorLights.gameObject.SetActive(false);
            }


            timerDisplay = GameObject.FindObjectOfType<TimerDisplay>();
        }
        
        //Train Speed
        if (TrainSpeedSlider != null)
            train.maxSpeedKph = TrainSpeedSlider.value;
        //Train Brake
        if (TrainBrakeSlider != null)
        {
            //TrainBrakeSlider.enabled = !train.automaticBrakes;
            //if (train.automaticBrakes)
            //    TrainBrakeSlider.value = train.brake;
            //else
                train.brake = TrainBrakeSlider.value;
        }
        
        if (TrainSpeedText != null)
        {
           // TrainSpeedText.text = train.Speed_KPH.ToString();
            TrainSpeedText.text = string.Format("{0}", train.Speed_KPH.ToString("0"));
        }
        if (tranBrake==true)
        {
            TrainSpeedText.text = string.Format("{0}", train.Speed_KPH.ToString("0"));
        }
        if (checkinterior == true && train != null)
        {
            TrainInteror = train.transform.Find("Train_Interior");
            TrainInteror.gameObject.SetActive(false);
           // FindObjectOfType<CameraChaing>().TrainLights(false);

            checkinterior =false;
            
        }
        
    }
    //For Starting the Train Engine
    public void EngingStart()
    {
        FindObjectOfType<GamePlaySound>().Btn_Sounds();
        if (train == null)
        {
            train = GameObject.FindGameObjectWithTag("Player").GetComponent<TrainController_v3>();

            
            timerDisplay = GameObject.FindObjectOfType<TimerDisplay>();

        }
        else
        {
        train.enginesOn = true;
            train.acceleration = 1f;
            train.GetComponent<Rigidbody>().isKinematic = false;

            train.transform.Find("SFX").gameObject.SetActive(true);
            //for Timmer
            timerDisplay.isTimeRunning = true;
            for (int i = 0; i < waggon.Length; i++)
            { 
                waggon[i].GetComponent<Rigidbody>().isKinematic = false;
            }
            if (train != null)
            {
                if (PlayerPrefs.GetFloat("musicSlider") == 0)
                {
                    train.transform.Find("SFX").gameObject.SetActive(false);
                }
                else
                {
                    train.transform.Find("SFX").gameObject.SetActive(true);
                }
            }

            FindObjectOfType<GamePlaySound>().BGSound();
        }
    }
    public void TrainBrake(bool check)
    {
        if (check==true)
        {
            train.enginesOn = false;
            tranBrake = true;
        }
        else
        {
            train.enginesOn = true;
            train.acceleration = 1f;
            tranBrake = false;
        }
    }
   public void TrainHorn()
    {
            train.Honk(); 
    }
    public void TrainLight()
    {
        train.ToggleLights();
    }
    public void TrainInterior(bool check)
    {
        if (train!=null)
        {
        TrainInteror = train.transform.Find("Train_Interior");
        TrainInteror.gameObject.SetActive(check);
         FindObjectOfType<CameraChaing>().TrainLights(check);
        }
        else
        {
            checkinterior = true;
        }
    }

   
    #endregion

    */
}
    

