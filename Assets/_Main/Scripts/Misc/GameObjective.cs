using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjective : MonoBehaviour
{
    [SerializeField]
    private Text ObjectiveText;
    [SerializeField]
    private Text DetailText;
    [SerializeField]
    private ObjectiveAndDetails[] objective;

    void Start()
    {

       
            ObjectiveText.text = objective[PlayerPrefs.GetInt("Level_No")].Objectives;
            DetailText.text = objective[PlayerPrefs.GetInt("Level_No")].ObjectivesDetail;
           
       
    }
    public void ObjecttobeOn()
    {
        for (int i = 0; i < objective[PlayerPrefs.GetInt("Level_No")].ThingstoOn.Length; i++)
        {
        objective[PlayerPrefs.GetInt("Level_No")].ThingstoOn[i].SetActive(true);
        }
        for (int i = 0; i < objective[PlayerPrefs.GetInt("Level_No")].ThingstoOff.Length; i++)
        {
            objective[PlayerPrefs.GetInt("Level_No")].ThingstoOff[i].SetActive(false);
        }
    }
}
[System.Serializable]
public class ObjectiveAndDetails
{
    
    public string Objectives;
    
    public string ObjectivesDetail;

    public GameObject[] ThingstoOn;
    public GameObject[] ThingstoOff;
}
