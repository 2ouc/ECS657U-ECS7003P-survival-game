using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundsControl : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource music;
    private float value=0.3f;
    public Slider musicslider;

    void Start()
    {
        music = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("music Value:"+ musicslider.value);
      //  music.volume = musicslider.value;
    }
    public void SliderMusic()
    {
        Debug.Log("music Value:" + musicslider.value);
        music.volume = musicslider.value;
        PlayerPrefs.SetFloat("MusicValue", musicslider.value);
    }
    public void ToggleMusic()
    {
        if(music.enabled == true)
        {
            music.enabled = false;
        }
        else if(music.enabled == false)
        {
            music.enabled = true;
        }
    }
    public void AutoHeadShot()
    {
        if (PlayerPrefs.GetInt("AutoShoot") == 1)
        {
            PlayerPrefs.SetInt("AutoShoot",0);
        }
        else if (PlayerPrefs.GetInt("AutoShoot") ==0)
        {
            PlayerPrefs.SetInt("AutoShoot",1);
        }
    }

}
