using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllChecker : MonoBehaviour
{
    public static bool train_Trigger=true;

    private void Start()
    {
        train_Trigger = true;
    }
    private void OnEnable()
    {
        train_Trigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.CompareTag("Player"))
        {
            train_Trigger = false;
       

        }
    }
    public void TriggerCheckker(int value)
    {
        train_Trigger = true;
        
        SwtichController.instance.Controll_Assiging(value);
        
    }

    
}
