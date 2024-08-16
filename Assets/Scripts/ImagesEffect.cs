using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Image AimEdges;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((ControlFreak2.CF2Input.GetButton("Fire1"))){
         //   Debug.Log("Fire Pressed");
            iTween.ShakeScale(AimEdges.gameObject,new Vector3(0f,1f), 0.1f);
           

        }
    }
    public void ChangeBackSize()
    {
        Debug.Log("Oncomplete Calling");
        iTween.ScaleBy(AimEdges.gameObject, iTween.Hash("x", 1, "z", 1, "default", .1));
    }
   
}
