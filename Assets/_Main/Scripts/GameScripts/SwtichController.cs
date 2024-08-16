using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SwtichController : MonoBehaviour
{
    public GameObject[] RailRoad_Colliders;

    public Image LeftTrackSelected;
    public Image RightTrackSelected;

    public GameObject Left_Collider;
    public GameObject Right_Collider;

    public static SwtichController instance;
    private void Start()
    {
        
        Controll_Assiging(0);
       // Left_Right_Switching(0);
    }
    private void OnEnable()
    {
        instance = this;
        Controll_Assiging(0);
    }

    public void Controll_Assiging(int num)
    {
        for (int i = 0; i < RailRoad_Colliders.Length; i++)
        {
            if (num == 0) //0
            {
                Left_Collider = RailRoad_Colliders[0];
                Right_Collider = RailRoad_Colliders[1];
            }
            else if (num == 1) //1
            {
                Left_Collider = RailRoad_Colliders[2];
                Right_Collider = RailRoad_Colliders[3];
            }
            else if (num == 2) //2
            {
                Left_Collider = RailRoad_Colliders[4];
                Right_Collider = RailRoad_Colliders[5];
            }
            else if (num == 3) //3
            {
                Left_Collider = RailRoad_Colliders[6];
                Right_Collider = RailRoad_Colliders[7];
            } 
            else if (num == 4) //4
            {
                Left_Collider = RailRoad_Colliders[8];
                Right_Collider = RailRoad_Colliders[9];
            }
            else if (num == 5) //5
            {
                Left_Collider = RailRoad_Colliders[9];
                Right_Collider = RailRoad_Colliders[10];
            }
            else if (num == 6) //6
            {
                Left_Collider = RailRoad_Colliders[9];
                Right_Collider = RailRoad_Colliders[10];
            }
        }
    }
    public void Left_Right_Switching(int num)
    {
        FindObjectOfType<GamePlaySound>().Btn_Sounds();
        if (ControllChecker.train_Trigger == true)
        {
            switch (num)
            {
                case 0:
                    Left_Collider.SetActive(true);
                    Right_Collider.SetActive(false);
                    LeftTrackSelected.gameObject.SetActive(true);
                    RightTrackSelected.gameObject.SetActive(false);
                    break;
                case 1:
                    Left_Collider.SetActive(false);
                    Right_Collider.SetActive(true);
                    LeftTrackSelected.gameObject.SetActive(false);
                    RightTrackSelected.gameObject.SetActive(true);
                    break; 
            }
        }     
    }   
}
