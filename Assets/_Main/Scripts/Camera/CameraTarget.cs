using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;
//using WSMGameStudio.RailroadSystem;
public class CameraTarget : MonoBehaviour
{
    [Header("Camera Target Name")]
    [SerializeField]
    private string ParentTargetName;
    [SerializeField]
    private string TargetName;
    [Header("Train Virtual Camera")]
    [SerializeField]
    private Transform CmTarget;
   // private CinemachineVirtualCamera CmVCam;
   // public TrainController_v3 train;


    
    private void Start()
    {
       
       // CmVCam = gameObject.GetComponent<CinemachineVirtualCamera>(); 
       
        Invoke("TrainComponent", 0.3f);
    }
    private void OnEnable()
    {
        Invoke("TrainComponent", 0.3f);
    }
    
    void TrainComponent()
    {
       
        //train = GameObject.FindGameObjectWithTag("Player").GetComponent<TrainController_v3>();
        //CmTarget = train.transform.Find(ParentTargetName).gameObject.transform.Find(TargetName);
        //if (CmTarget!= null)
        //{
        //    CmVCam.Follow = CmTarget;
        //    CmVCam.LookAt = CmTarget;
        //}
        
    }

}
