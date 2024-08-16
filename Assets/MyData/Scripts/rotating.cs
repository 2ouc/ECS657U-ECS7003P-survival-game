using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float xAngle, yAngle, zAngle;
    public bool is_self=true;
    private float speed=50f;
   
    private void Start()
    {
       
    }
    void Update()
    {
       

        if (is_self)
        {
            this.gameObject.transform.Rotate(50f, yAngle, zAngle, Space.Self);
        }
        else
        {
            this.gameObject.transform.Rotate(50f, yAngle, zAngle, Space.World);
        }
        
    }

}
