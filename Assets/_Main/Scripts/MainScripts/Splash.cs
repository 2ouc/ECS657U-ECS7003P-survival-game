using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{

    public GameObject PrivacypolicyPanel, LoadingScreen;

   
    void Start()
    {

     
        if (!PlayerPrefs.HasKey("PrivacyChecked"))
        {
            PrivacypolicyPanel.SetActive(true);
        }
        else
        {
            PrivacypolicyPanel.SetActive(false);
            LoadingScreen.SetActive(true);
            SceneManager.LoadScene("MainMenu");
        }
        PlayerPrefs.SetInt("PrivacyChecked", 1);
        PrivacypolicyPanel.SetActive(false);
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
    public void OK()
    {
        PlayerPrefs.SetInt("PrivacyChecked", 1);
        PrivacypolicyPanel.SetActive(false);
        LoadingScreen.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
}
