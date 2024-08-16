using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;


public class CameraChaing : MonoBehaviour
{
   

    public GameObject[] cameraTargets;

   // public TrainController_v3 train;

    Transform TrainInteror;
    Transform TrainInteriorLights;
    Transform TrainExteriorLights;


    void Start()
    {
       
        Invoke("CameraTarget", 0.3f);
       
    }
    private void OnEnable()
    {
        

        Invoke("CameraTarget", 0.3f);
    }


    void CameraTarget()
    {
        //train = GameObject.FindGameObjectWithTag("Player").GetComponent<TrainController_v3>();
        //TrainInteror = train.transform.Find("Train_Interior");

        //TrainInteriorLights = train.transform.Find("Lights").gameObject.transform.Find("TrainNewLights").gameObject.transform.Find("Interiorlight");
        //TrainExteriorLights = train.transform.Find("Lights").gameObject.transform.Find("TrainNewLights").gameObject.transform.Find("ExteriorLight");


        
        cameraTargets[1].SetActive(false);
        cameraTargets[2].SetActive(false);
    }

    public void CameraSwitching(int num)
    {
        FindObjectOfType<GamePlaySound>().Btn_Sounds();
        switch (num)
        {
            case 0:
                cameraTargets[0].SetActive(true);
                cameraTargets[1].SetActive(false);
                cameraTargets[2].SetActive(false);
                TrainInteror.gameObject.SetActive(true);
                if (PlayerPrefs.GetInt("Mode_no") == 4)
                {
                TrainInteriorLights.gameObject.SetActive(true);
                TrainExteriorLights.gameObject.SetActive(false);

                }
                break;
            case 1:
                cameraTargets[1].SetActive(true);
                cameraTargets[0].SetActive(false);
                cameraTargets[2].SetActive(false);
                
                TrainInteror.gameObject.SetActive(false);
                if (PlayerPrefs.GetInt("Mode_no") == 4)
                {
                    TrainInteriorLights.gameObject.SetActive(false);
                    TrainExteriorLights.gameObject.SetActive(true);
                }
                break;
            case 2:
                cameraTargets[2].SetActive(true);
                cameraTargets[0].SetActive(false);
                cameraTargets[1].SetActive(false);
                
                TrainInteror.gameObject.SetActive(false);
                if (PlayerPrefs.GetInt("Mode_no") == 4)
                {
                    TrainInteriorLights.gameObject.SetActive(false);
                    TrainExteriorLights.gameObject.SetActive(true);
                }
                break;

            default:
                break;
        }

    }

    public void TrainLights(bool check)
    {
      
          //  TrainInteriorLights.gameObject.SetActive(check);
        

          
        
    }
}
