using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingLevelController : MonoBehaviour {

    public GameObject LoadingImage, TabToContinueBtn;//,RemoveAdsPanel;
    public Slider LoadingSlider;
    public static LoadingLevelController instance;
    AsyncOperation operations;
    // Use this for initialization
    void Start () {
		instance = this;
        TabToContinueBtn.SetActive(false);
        //Yuneeb
        //if (AdsManager.Instance)
        //{
        //    AdsManager.Instance.TopBanner();


        //}
    }
    public void LoadLevel(string name)
	{
        //if (PlayerPrefs.GetInt("remove_ads") == 1)
        //{
        //    RemoveAdsPanel.SetActive(false);
        //}
		StartCoroutine (WaitLoaing(name));
	}
	IEnumerator WaitLoaing(string name)
	{
		LoadingImage.SetActive (true);
        Debug.Log("Scene Name is : "+name);
		operations = SceneManager.LoadSceneAsync (name);

        operations.allowSceneActivation = false;
        //operations.
        while (!operations.isDone) {
			float progress= Mathf.Clamp01(operations.progress/0.9f);
            LoadingSlider.value = progress;
            if (progress >= 0.95)
            {
                //Debug.Log("Done");
                StartCoroutine(TapToContinue());
                TabToContinueBtn.SetActive(true);
            }
            yield return null;
		}
    }
    IEnumerator TapToContinue()
    {
        yield return new WaitForSeconds(10f);
        operations.allowSceneActivation = true;
        
    }
}
