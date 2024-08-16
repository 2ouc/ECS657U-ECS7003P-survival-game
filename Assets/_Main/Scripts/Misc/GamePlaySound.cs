using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySound : MonoBehaviour
{
   
    [Header("Audio Source for Music")]
    [SerializeField]
    private AudioSource music;
    [Header("Audio Source for Sound")]
    [SerializeField]
    private AudioSource sound;
    [Header("Audio Source for LevelFailed")]
    [SerializeField]
    private AudioSource levelFailed;
    void Start()
    {
        
        if (PlayerPrefs.GetFloat("musicSlider")>0)
        {
          
        music.volume = PlayerPrefs.GetFloat("musicSlider");

        sound.volume = PlayerPrefs.GetFloat("soundSlider");

        }
        else
        {
            music.volume = 0f;
            sound.volume = 0f;

        }
    }
    void OnEnable()
    {
        if (PlayerPrefs.GetFloat("musicSlider") > 0)
        {
            music.volume = PlayerPrefs.GetFloat("musicSlider");
            sound.volume = PlayerPrefs.GetFloat("soundSlider");
        }
        else
        {
            music.volume = 0f;
            sound.volume = 0f;

        }
    }

    public void BGSound()
    {
        music.Play();
        sound.Play();
    }

    public void StopLevelCompleteBGSound()
    {
        music.Stop();
        
    }
    public void StopLevelFailedBGSound()
    {
        music.Stop();
       if (PlayerPrefs.GetFloat("musicSlider") == 0)
        {
        levelFailed.Stop();

        }
        else
        {
            levelFailed.Play();
        }
    }

    public void Btn_Sounds()
    {
        if (PlayerPrefs.GetFloat("soundSlider") > 0f)
        {
            sound.Play();
        }
    }
   
}
