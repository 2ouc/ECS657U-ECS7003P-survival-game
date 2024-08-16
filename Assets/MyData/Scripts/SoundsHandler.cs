using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsHandler : MonoBehaviour
{
    public AudioClip DoorOpenClip, DoorCloseClip;


    public static SoundsHandler instance;
    // Start is called before the first frame update
    void OnEnable()
    {
        instance = this;
    }
}
