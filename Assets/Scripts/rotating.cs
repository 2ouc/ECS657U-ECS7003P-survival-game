using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotating : MonoBehaviour
{
    // Start is called before the first frame update
    public float xAngle, yAngle, zAngle;
    public bool is_self=false;

    void Update()
    {

        if (is_self)
        {
            this.gameObject.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
        }
        else
        {
            this.gameObject.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
        }
        
    }

}
