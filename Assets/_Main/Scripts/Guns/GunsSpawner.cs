using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunsSpawner : MonoBehaviour
{
    [SerializeField]
    private GunsData[] gunsData;
    [SerializeField]
    private int grenade;
    [SerializeField]
    private float lastGrenade;
   
    
    void Start()
    {
        if (gunsData[GunsSelection.currentGun].Selected==true)
        {
           Instantiate(gunsData[GunsSelection.currentGun].GamePlayModel, gameObject.transform.position, gameObject.transform.rotation);
            
        }
    }
}
