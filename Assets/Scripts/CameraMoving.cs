using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform[] WayPoints;
    float speed = 2f;
    public Transform targetObject;
    public Vector3 cameraOffset;
    public float smoothFactor = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("path", new Vector3[] { new Vector3(-0.335f, 0.267f, 0.999f), new Vector3(0.184f, 0.046f, -0.98f), new Vector3(-0.059f, 0.21f, -1.156f) }, "time", 5));
        //  cameraOffset = transform.position - GameObject.FindGameObjectWithTag("ActionBullet").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       

    }

}
