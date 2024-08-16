using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;
//using WSMGameStudio.RailroadSystem;
public class FreeLookCameraTarget : MonoBehaviour
{
    [Header("Camera Target Name")]
    [SerializeField]
    private string ParentTargetName;
    [SerializeField]
    private string TargetName;
    [Header("Train FreeLook Camera")]
    [SerializeField]
    private Transform CmFreeeLookTarget;
    //private CinemachineFreeLook CmFreeLook;
    //private TrainController_v3 train;

    private Transform cameraTransform;

    private void Start()
    {

      //  CmFreeLook = gameObject.GetComponent<CinemachineFreeLook>(); 
       
        Invoke("TrainComponent", 0.2f);
    }
    private void OnEnable()
    {
        Invoke("TrainComponent", 0.2f);
    }

    void TrainComponent()
    {

        //train = GameObject.FindGameObjectWithTag("Player").GetComponent<TrainController_v3>();
        //CmFreeeLookTarget = train.transform.Find(ParentTargetName).gameObject.transform.Find(TargetName);
       
       

        //if (CmFreeeLookTarget != null)
        //{
        //    CmFreeLook.Follow = CmFreeeLookTarget;
        //    CmFreeLook.LookAt = CmFreeeLookTarget;
        //}
        
    }

}
