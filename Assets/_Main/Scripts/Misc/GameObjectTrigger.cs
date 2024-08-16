using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectTrigger : MonoBehaviour
{
    [SerializeField]
    private string TagName;
    [SerializeField]
    private UnityEvent onTriggerEnter;
    [SerializeField]
    private UnityEvent onTriggerStay;
    [SerializeField]
    private UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TagName))
        {

        onTriggerEnter.Invoke();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //    if (other.gameObject.CompareTag(TagName))
        //    {
        //        onTriggerStay.Invoke();
        //    }
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag(TagName))
        //{
        //    onTriggerExit.Invoke();
        //}
    }

}
