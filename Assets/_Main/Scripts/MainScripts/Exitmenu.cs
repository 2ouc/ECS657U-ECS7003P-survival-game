using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exitmenu : MonoBehaviour
{
   public void GameExit()
    {
       // FindObjectOfType<MainMenuSound>().BtnSoundPlay();
        Application.Quit();
    }
    public void BtnSound()
    {
        FindObjectOfType<MainMenuSound>().BtnSoundPlay();
    }
}
