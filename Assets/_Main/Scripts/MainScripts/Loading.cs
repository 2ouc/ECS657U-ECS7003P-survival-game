using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    //public Slider fill_bar;
    //public Text prctage_txt;
    public int scen_index;
    AsyncOperation operation;
    [HideInInspector]
  // public float waitTime = 3f;
    //private int no;
    public bool is_gamePlay = false;
    private void Start()
    {
        //fill_bar.value = 0.0f;
        //Time.timeScale = 1.0f;
        //operation.allowSceneActivation = false;
       StartCoroutine(load_scen(scen_index));

    }

    public void StartLoadin()
    {
        operation = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("Mode_no"));
        operation.allowSceneActivation = false;
    }

    public void LoadScene()
    {
    operation.allowSceneActivation = true;
    }

    IEnumerator load_scen(int n)
    {
        operation = SceneManager.LoadSceneAsync(scen_index);
        operation.allowSceneActivation = true;

        ////while (!operation.isDone)
        ////{
            //if (fill_bar.value == 1.0f)
            //{
            //   // operation.allowSceneActivation = true;
            //    Debug.Log("Progress Value in if" + operation.progress);
            //}
            //else
            //{
            // fill_bar.value += 1.0f / waitTime * Time.deltaTime;


            ////Debug.Log("Progress Value" + operation.progress);

            ////fill_bar.value += Mathf.Clamp01(operation.progress / .9f) * Time.deltaTime;
            ////Debug.Log("fill_bar value" + fill_bar.value);

            ////prctage_txt.text = "" + (int)((fill_bar.value) * 100) + " %";

            //}
            yield return null;
        //}


    }


}