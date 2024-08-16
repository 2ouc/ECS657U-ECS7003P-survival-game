using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnOff : MonoBehaviour
{
    [Header("Audio Source for Music")]
    [SerializeField]
    private AudioSource music;
    [Header("Audio Source for Sound")]
    [SerializeField]
    private AudioSource sound;
    [Header("Buttons of Sound")]
    [SerializeField]
    private Button SoundOn;
    [SerializeField]
    private Button SoundOff;

    

    private void Start()
    {
        SoundON();
    }

    public void SoundON()
    {
        music.Play();
        sound.Play();
        SoundOn.gameObject.SetActive(true);
        SoundOff.gameObject.SetActive(false);
        PlayerPrefs.SetFloat("musicSlider",0.6f);
            PlayerPrefs.SetFloat("soundSlider",0.6f);
        
    }
    public void SoundOFF()
    {
        music.Pause();
        sound.Pause();
        SoundOff.gameObject.SetActive(true);
        SoundOn.gameObject.SetActive(false);
        PlayerPrefs.SetFloat("musicSlider", 0);
        PlayerPrefs.SetFloat("soundSlider", 0);
    }
}
