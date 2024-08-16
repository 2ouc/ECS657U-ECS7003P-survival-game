using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public GameObject TimerObject;
    public Text TimerText;
    public static TimerController instance;
    //[HideInInspector]
    public float GameTime;
    float minutes,seconds;
    //[HideInInspector]
    public bool isStart;
    //[HideInInspector]
    public bool timeOver;
    public GameObject TimeImage;
    float consumeTime;
    float RecordTime;
    // Start is called before the first frame update
    private void Start()
    {
        RecordTime = GameTime;
      //  Debug.Log("MissionStart Time Is:"+ RecordTime);
    }
    void OnEnable()
    {
        instance=this;
    }
    public void StartTimer(float gameTime)
    {
        isStart=true;
        GameTime=gameTime;
        TimerObject.SetActive(true);
    }
    public void AddTime(int time)
    {
        GameTime += time;
    }
    public void StopTimer()
    {
        isStart = false;
        timeOver = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(isStart && !timeOver)
        {
            GameTime-=Time.deltaTime;
            minutes=GameTime/60f;
            seconds=GameTime%60f;
           
            consumeTime = GameTime / RecordTime;
           
           // Debug.Log("My Game Time:"+ consumeTime);
           
            TimerText.text=string.Format("{00:00}:{01:00}",(int)minutes,(int)seconds);
           
          
            TimeImage.GetComponent<Image>().fillAmount = consumeTime;
            //Working On Time Roller Here
            if (GameTime <= 0)
            {
                timeOver = true;
                Time.timeScale = 0;
                GameManager.instance.MissionFailPanel.SetActive(true);
               
                StartCoroutine(StartAnimation());
            }
        }
        else
        {
           // TimerObject.SetActive(false);
        }
    }
    IEnumerator StartAnimation()
    {
        if (TimerObject.GetComponent<Animation>())
        {
            TimerObject.GetComponent<Animation>().enabled = true;
        }

        yield return new WaitForSeconds(1f);
        
    
    }
   
}
