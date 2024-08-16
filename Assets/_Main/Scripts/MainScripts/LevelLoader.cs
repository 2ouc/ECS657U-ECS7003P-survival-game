using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LevelLoader : MonoBehaviour
{
    public GameObject[] Levels;
    public Transform[] SpawnPoints;


    void Start()
    {
        
                Instantiate(Levels[PlayerPrefs.GetInt("Level_No")], SpawnPoints[PlayerPrefs.GetInt("Level_No")].transform.position, SpawnPoints[PlayerPrefs.GetInt("Level_No")].transform.rotation);
        Analytics.CustomEvent("GamePlay", new Dictionary<string, object>
            {
                { "level Start", PlayerPrefs.GetInt("Level_No")}
            });
       
    
    
    }

}
