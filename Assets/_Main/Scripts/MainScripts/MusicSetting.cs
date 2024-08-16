using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSetting : MonoBehaviour
{
    [Header("Sliders for Controlling Music")]
    [SerializeField]
    private Slider musicSlider;
    [Header("Sliders for Controlling Souund")]
    [SerializeField]
    private Slider soundSlider;
    [Header("Audio Source for Music")]
    [SerializeField]
    private AudioSource music;
    [Header("Audio Source for Sound")]
    [SerializeField]
    private AudioSource sound;

    void Start()
    {
        UpdateUI();
    }

    public void musicVolume(float vol)
    {
         PlayerPrefs.SetFloat("musicSlider", vol);
        
        UpdateUI();
    }
    public void soundVolume(float vol)
    {
         PlayerPrefs.SetFloat("soundSlider", vol);
        UpdateUI();
    }

    public void UpdateUI()
    {
       
       
        musicSlider.value = PlayerPrefs.GetFloat("musicSlider");
       
        music.volume= PlayerPrefs.GetFloat("musicSlider");
        soundSlider.value = PlayerPrefs.GetFloat("soundSlider");

        sound.volume= PlayerPrefs.GetFloat("soundSlider");
    }

    public void BtnSound()
    {
        FindObjectOfType<MainMenuSound>().BtnSoundPlay();
    }
}
