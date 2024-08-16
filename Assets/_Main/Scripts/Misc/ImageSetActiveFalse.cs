using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSetActiveFalse : MonoBehaviour
{
   
   public void ImageFalse()
    {
        gameObject.GetComponent<Image>().gameObject.SetActive(false);
        
    }

    
}
