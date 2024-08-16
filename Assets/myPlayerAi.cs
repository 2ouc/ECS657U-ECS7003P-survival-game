using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myPlayerAi : MonoBehaviour
{
    public static myPlayerAi instance;
    // Start is called before the first frame update
    public GameObject currentTarget;
    public GameObject fpcamera;
    public bool allowTargetAim = false;
    public bool aiming = false;
    void Start()
    {
        Invoke("disableTarget", 1f);
    }
    private void Awake()
    {
        instance = this;
    }
    void disableTarget()
    {
        currentTarget = null;
        flaw();

    }
    void flaw()
    {
        Invoke("disableTarget", 1f);
    }
    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            findNewTarget();
        }
        
        if (ControlFreak2.CF2Input.GetAxis("Mouse Y") == 0)
        {
            if (currentTarget != null)
            {
                float distance = Vector3.Distance(this.gameObject.transform.position, currentTarget.transform.position);
                Vector3 MytargetDirection = (currentTarget.transform.position - this.transform.position).normalized;
                RaycastHit hit;
                if (allowTargetAim)
                {
                    if (Physics.Raycast(currentTarget.transform.position + Vector3.up - (MytargetDirection), -MytargetDirection, out hit))
                    {
                        if (hit.transform.gameObject.tag == "Player")
                        {

                            if (distance < 16)
                            {
                                takeAim();
                                //Debug.LogError("aiming");
                            }
                            else
                            {
                                currentTarget = null;
                            }

                        }
                        else
                            currentTarget = null;
                    }
                    else
                        currentTarget = null;
                }
                else
                {
                    currentTarget = null;
                }
            }
        }
        else
        {
            currentTarget = null;
        }
        void findNewTarget()
        {
            currentTarget = null;
            for (int i = 0; i < 1; i++)
            {
                GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
                float closerDistance = float.MaxValue;
                for (int t = 0; t < targets.Length; t++)
                {
                    if (targets[t] != null && targets[t] != this.gameObject)
                    {
                        float distance = Vector3.Distance(this.gameObject.transform.position, targets[t].transform.position);
                        if (distance < closerDistance)
                        {
                            currentTarget = targets[t];
                            closerDistance = distance;
                        }
                    }
                }
            }
            if (currentTarget != null)
            {
                camAimControl();
            }
        }
        void takeAim()
        {
            Vector3 relativePos = currentTarget.transform.position - fpcamera.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            fpcamera.transform.rotation = Quaternion.Slerp(fpcamera.transform.rotation, rotation, Time.deltaTime * 3);
        }
        void camAimControl()
        {
           
            {
                Vector3 targetVpPos = fpcamera.GetComponent<Camera>().WorldToViewportPoint(currentTarget.transform.position);
                if (targetVpPos.x > 0.2 && targetVpPos.x < 0.6 && targetVpPos.z > 0)
                {
                    myPlayerAi.instance.allowTargetAim = true;
                }
                else
                {
                    //myPlayerAi.instance.allowTargetAim = false;
                    currentTarget = null;
                }

            }
        }
    }
}
