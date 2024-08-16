using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private GameObject LoadingScreen;
    [SerializeField]
    private Slider ProgressbarSlider;
    [SerializeField]
    private Text progressText;


    public void StartLoading()
    {
        StartCoroutine(LoadingScene());
    }
    IEnumerator LoadingScene ()
    {
        LoadingScreen.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        AsyncOperation operation = asyncOperation;
        while (!operation.isDone)
        {
            ProgressbarSlider.value = operation.progress ;
            progressText.text = operation.progress.ToString() +"%";

            //float progress = Mathf.Clamp01(operation.progress / .9f) *Time.unscaledDeltaTime;
            //ProgressbarSlider.value = progress;
            //progressText.text =(int) (progress * 100) + "%";
            //Debug.Log("Progress Value" + progress);
            yield return null;
        }
    }
}
