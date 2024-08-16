using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObectsToDestroy : MonoBehaviour
{
   
    public DestroyGameObjectData[] objectToDestry;
    void Start()
    {
      
        for (int i = 0; i < objectToDestry[PlayerPrefs.GetInt("Level_No")].DestrObj.Length; i++)
        {
            Destroy(objectToDestry[PlayerPrefs.GetInt("Level_No")].DestrObj[i]);
        }
    }
}

[System.Serializable]
public class DestroyGameObjectData
{
    public GameObject[] DestrObj;
}

