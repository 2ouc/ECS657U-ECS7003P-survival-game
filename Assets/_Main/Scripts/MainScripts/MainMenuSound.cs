using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSound : MonoBehaviour
{
    [Header("Audio Source for Music")]
    [SerializeField]
    private AudioSource music;
    [Header("Audio Source for Sound")]
    [SerializeField]
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BtnSoundPlay()
    {
        if (PlayerPrefs.GetFloat("soundSlider")>0f)
        {
            sound.Play();


        }
    }
}
