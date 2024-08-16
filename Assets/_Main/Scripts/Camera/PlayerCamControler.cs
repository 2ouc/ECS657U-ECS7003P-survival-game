using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamControler : MonoBehaviour
{
    public float speedH;
    public float speedV;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


    private void Update()
    {
        pitch -= speedV * Input.GetAxis("Mouse Y");
        yaw += speedH * Input.GetAxis("Mouse X");

        pitch = Mathf.Clamp(pitch, -90f, 90f);
        while(yaw < 0f)
        {
            yaw += 360f;
        }
        while (yaw >= 360f)
        {
            yaw -= 360f;
        }



        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
