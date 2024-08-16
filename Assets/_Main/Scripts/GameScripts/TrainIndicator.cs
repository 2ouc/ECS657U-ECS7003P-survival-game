using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class TrainIndicator : MonoBehaviour
{
    public Button[] arrowImage;
    public Button[] LeftRightButtons;
    [Header("Left = 0 and Right = 1")]
    public int value;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            arrowImage[value].gameObject.SetActive(true);
            LeftRightButtons[value].interactable = false;
        }
       
    }
    public void ArrowButton(int Value)
    {
        FindObjectOfType<GamePlaySound>().Btn_Sounds();
        Time.timeScale = 1;
        LeftRightButtons[value].interactable = true;
        SwtichController.instance.Left_Right_Switching(Value);
        arrowImage[Value].gameObject.SetActive(false);
    }

}
