using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 6.0f * 5f * Time.deltaTime, 0,Space.Self);
    }
}
